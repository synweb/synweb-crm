using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SynWebCRM.Contract.Models;
using SynWebCRM.Contract.Repositories;
using SynWebCRM.Data.EF;
using SynWebCRM.Web.Helpers;
using SynWebCRM.Web.Models;
using SynWebCRM.Web.Security;

namespace SynWebCRM.Web.Controllers
{
    [Authorize(Roles = CRMRoles.Admin + "," + CRMRoles.Sales)]
    public class CustomersController : Controller
    {
        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        
        private readonly ICustomerRepository _customerRepository;
       

        // GET: Customers
        public ActionResult Index()
        {
            var customers = _customerRepository.AllIncluding(x => x.Deals).OrderByDescending(x => x.NeedsAttention)
                .ThenByDescending(x => x.CreationDate);
            return View(customers);
        }
        
        // GET: Customers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);  
            }
            Customer customer = _customerRepository.GetById(id.Value);
            if (customer == null)
            {
                return StatusCode(404);
            }
            ICollection<Deal> deals = customer.Deals.OrderByDescending(x => x.CreationDate).ToList();
            ViewBag.Deals = deals;
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            var model = new Customer();
            model.CreationDate = DateTime.Now;
            return View(model);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("CustomerId,CreationDate,Name,Source,Description,Phone,Email,VkId,NeedsAttention")] Customer customer)
        {
            if (!string.IsNullOrEmpty(customer.VkId))
            {
                customer.VkId = ParseHelper.ParseVK(customer.VkId);
            }
            if (ModelState.IsValid)
            {
                customer.Creator = User.Identity.Name;
                _customerRepository.Add(customer);
                return RedirectToAction("Details", new {id = customer.CustomerId});
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return StatusCode(404);
            }
            Customer customer = _customerRepository.GetById(id.Value);
            if (customer == null)
            {
                return StatusCode(400);
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("CustomerId,Creator,CreationDate,Name,Source,Description,Phone,Email,VkId,NeedsAttention")] Customer customer)
        {
            if (!string.IsNullOrEmpty(customer.VkId))
            {
                customer.VkId = ParseHelper.ParseVK(customer.VkId);
            }
            if (ModelState.IsValid)
            {
                _customerRepository.Update(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);
            }
            Customer customer = _customerRepository.GetById(id.Value);
            if (customer == null)
            {
                return StatusCode(404);
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = _customerRepository.GetById(id);
            _customerRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Rating()
        {
            var customers = _customerRepository.AllWithDeals();
            List<CustomerChartElement> model = new List<CustomerChartElement>();
            foreach (var customer in customers)
            {
                var deals = customer.Deals.Where(x => x.DealState.IsCompleted).OrderBy(x => x.CreationDate).ToList();
                var orderCount = deals.Count();
                double recency = 0;
                double period = 0;
                decimal summary = 0;
                if (deals.Any())
                {
                    recency = (int) (DateTime.Now - deals.Last().CreationDate).TotalDays;
                    summary = deals.Where(x => x.Profit.HasValue).Sum(x => x.Profit.Value);
                    if(deals.Count > 1)
                    {
                        List<double> periods = new List<double>();
                        for(int i = 1; i< deals.Count; i++)
                        {
                            periods.Add((deals[i].CreationDate - deals[i - 1].CreationDate).TotalDays);
                        }
                        period = periods.Average();
                    }
                    else
                    {
                        period = recency;
                    }
                }
                if(summary == 0)
                    continue;
                if(recency > 365)
                    continue;

                

                model.Add(new CustomerChartElement()
                {
                    Customer = customer,
                    Recency = recency,
                    OrderCount = orderCount,
                    Period = period,
                    Summary = summary,
                    RatingValue = GetRatingValue(recency, period, summary)
                });
            }

            double max = model.Max(x => x.RatingValue);
            double avg = model.Average(x => x.RatingValue);
            double low = avg/2;
            double high = (max - avg)/2;    
            foreach (var customer in model.Where(x => x.RatingValue <= low))
            {
                customer.Rating = ClientRating.B;
            }
            foreach (var customer in model.Where(x => x.RatingValue > low && x.RatingValue <= avg))
            {
                customer.Rating = ClientRating.A;
            }
            foreach (var customer in model.Where(x => x.RatingValue > avg && x.RatingValue <= high))
            {
                customer.Rating = ClientRating.AA;
            }
            foreach (var customer in model.Where(x => x.RatingValue > high))
            {
                customer.Rating = ClientRating.AAA;
            }

            return View(model);
        }

        private double GetRatingValue(double recency, double period, decimal summary)
        {
            const double SUMMARY_WEIGHT = 1.0d;
            const double RECENCY_WEIGHT = 0.2d;
            const double PERIOD_WEIGHT = 0.5d;

            double summaryPart = Math.Pow((double) summary, SUMMARY_WEIGHT);
            double recencyPart = Math.Pow(1 / (1 + Math.Floor(recency / 30)), RECENCY_WEIGHT);
            double periodPart = Math.Pow(1 / (1 + Math.Floor(period / 14)), PERIOD_WEIGHT);
            var res = summaryPart * recencyPart * periodPart;
            return res;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
