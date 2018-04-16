using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SynWebCRM.Web.Security;
using SynWebCRM.Data.EF;
using SynWebCRM.Contract.Models;
using SynWebCRM.Contract.Repositories;
using SynWebCRM.Web.Models;

namespace SynWebCRM.Web.Controllers
{
    [Authorize(Roles = CRMRoles.Admin + "," + CRMRoles.Sales)]
    public class DealsController : Controller
    {
        private readonly IDealRepository _dealRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDealStateRepository _dealStateRepository;
        private readonly IServiceTypeRepository _serviceTypeRepository;

        public DealsController(IDealRepository dealRepository, ICustomerRepository customerRepository, IDealStateRepository dealStateRepository, IServiceTypeRepository serviceTypeRepository)
        {
            _dealRepository = dealRepository;
            _customerRepository = customerRepository;
            _dealStateRepository = dealStateRepository;
            _serviceTypeRepository = serviceTypeRepository;
        }

        private SelectList GetCustomersSelectList(int? selectedId= null)
        {
            var res = new SelectList( _customerRepository.AllWithDeals().OrderByDescending(x => x.Deals.Count)
                    .Select(x => new SelectListItem()
                    {Text = $"{x.Name} ({x.CreationDate:dd.MM.yyyy})", Value = x.CustomerId.ToString()}),
                "Value", "Text", selectedId);
            return res;
        }

        // GET: Deals
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewDealWithCustomer()
        {
            ViewBag.ServiceTypeId = new SelectList(_serviceTypeRepository.All(), nameof(ServiceType.ServiceTypeId), nameof(ServiceType.Name));
            ViewBag.DealStateId = new SelectList(_dealStateRepository.All(), nameof(DealState.DealStateId), nameof(DealState.Name));

            var model = new NewDealWithCustomerModel(){Customer = new Customer(){CreationDate = DateTime.Now}, Deal = new Deal(){CreationDate = DateTime.Now}};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewDealWithCustomer(NewDealWithCustomerModel model)
        {
            return new RedirectToActionResult(nameof(Index), nameof(DealsController), null);
        }

        // GET: Deals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(404);
            }
            Deal deal = _dealRepository.GetById(id.Value);
            if (deal == null)
            {
                return StatusCode(400);
            }
            return View(deal);
        }

        // GET: Deals/Create
        public ActionResult Create(int? customerId)
        {
            ViewBag.CustomerId = GetCustomersSelectList(customerId);
            ViewBag.DealStateId = new SelectList(_dealStateRepository.All(), nameof(DealState.DealStateId), nameof(DealState.Name));
            ViewBag.ServiceTypeId = new SelectList(_serviceTypeRepository.All(), nameof(ServiceType.ServiceTypeId), nameof(ServiceType.Name));
            var dealTypes = (from DealType i in Enum.GetValues(typeof(DealType))
                             select new SelectListItem { Text = i.ToString(), Value = i.ToString() }).ToList();
            ViewBag.DealType = dealTypes;

            var model = new Deal();
            model.CreationDate = DateTime.Now;
            return View(model);
        }

        // POST: Deals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("DealId,CreationDate,Sum,Profit,CustomerId,ServiceTypeId,Name,Description,Type,DealStateId,NeedsAttention")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                deal.Creator = User.Identity.Name;
                _dealRepository.Add(deal);
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = GetCustomersSelectList();
            ViewBag.DealStateId = new SelectList(_dealStateRepository.All(), nameof(DealState.DealStateId), nameof(DealState.Name));
            ViewBag.ServiceTypeId = new SelectList(_serviceTypeRepository.All(), nameof(ServiceType.ServiceTypeId), nameof(ServiceType.Name));
            return View(deal);
        }

        // GET: Deals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);
            }
            Deal deal = _dealRepository.GetById(id.Value);
            if (deal == null)
            {
                return StatusCode(400);
            }
            ViewBag.CustomerId = GetCustomersSelectList(deal.CustomerId);
            ViewBag.DealStateId = new SelectList(_dealStateRepository.All(), nameof(DealState.DealStateId), nameof(DealState.Name));
            ViewBag.ServiceTypeId = new SelectList(_serviceTypeRepository.All(), nameof(ServiceType.ServiceTypeId), nameof(ServiceType.Name));
            return View(deal);
        }

        // POST: Deals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("DealId,Creator,CreationDate,ServiceTypeId,Sum,Profit,CustomerId,Name,Description,Type,DealStateId,NeedsAttention")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                _dealRepository.Update(deal);
                return RedirectToAction("Index");
            }

            return Edit(deal.DealId);
        }

        // GET: Deals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);
            }
            Deal deal = _dealRepository.GetById(id.Value);
            if (deal == null)
            {
                return StatusCode(400);
            }
            return View(deal);
        }

        // POST: Deals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _dealRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
