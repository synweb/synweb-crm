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

namespace SynWebCRM.Web.Controllers
{
    [Authorize(Roles = CRMRoles.Admin + "," + CRMRoles.Sales)]
    public class DealsController : Controller
    {
        public DealsController(CRMModel crmModel)
        {
            _crmModel = crmModel;
        }

        private readonly CRMModel _crmModel;
        private IEnumerable<Customer> Customers { get { return  _crmModel.Customers.OrderByDescending(x => x.CreationDate).ToList();} }
        
        private SelectList GetCustomersSelectList(int? selectedId= null)
        {
            var res = new SelectList(
                Customers.OrderByDescending(x => x.Deals.Count)
                    .ThenBy(x => x.Name)
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

        // GET: Deals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(404);
            }
            Deal deal = _crmModel.Deals
                .Include(x => x.Customer)
                .Include(x => x.Estimates)
                .Include(x => x.Notes)
                .Single(x => x.DealId == id);
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
            ViewBag.DealStateId = new SelectList(_crmModel.DealStates.OrderBy(x => x.Order), nameof(DealState.DealStateId), nameof(DealState.Name));
            ViewBag.ServiceTypeId = new SelectList(_crmModel.ServiceTypes.OrderBy(x => x.ServiceTypeId), nameof(ServiceType.ServiceTypeId), nameof(ServiceType.Name));
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
                _crmModel.Deals.Add(deal);
                _crmModel.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = GetCustomersSelectList();
            ViewBag.DealStateId = new SelectList(_crmModel.DealStates.OrderBy(x => x.Order), nameof(DealState.DealStateId), nameof(DealState.Name));
            ViewBag.ServiceTypeId = new SelectList(_crmModel.ServiceTypes.OrderBy(x => x.ServiceTypeId), nameof(ServiceType.ServiceTypeId), nameof(ServiceType.Name));
            return View(deal);
        }

        // GET: Deals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);
            }
            Deal deal = _crmModel.Deals.Find(id);
            if (deal == null)
            {
                return StatusCode(400);
            }
            ViewBag.CustomerId = GetCustomersSelectList(deal.CustomerId);
            ViewBag.DealStateId = new SelectList(_crmModel.DealStates.OrderBy(x => x.Order), nameof(DealState.DealStateId), nameof(DealState.Name), deal.DealStateId);
            ViewBag.ServiceTypeId = new SelectList(_crmModel.ServiceTypes.OrderBy(x => x.ServiceTypeId), nameof(ServiceType.ServiceTypeId), nameof(ServiceType.Name), deal.ServiceTypeId);
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
                _crmModel.Entry(deal).State = EntityState.Modified;
                _crmModel.SaveChanges();
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
            Deal deal = _crmModel.Deals.Find(id);
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
            Deal deal = _crmModel.Deals.Find(id);
            _crmModel.Deals.Remove(deal);
            _crmModel.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _crmModel.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
