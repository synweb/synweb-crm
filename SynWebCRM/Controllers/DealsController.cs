using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SynWebCRM.Data;
using SynWebCRM.Security;

namespace SynWebCRM.Controllers
{
    [Authorize(Roles = CRMRoles.Admin + "," + CRMRoles.Sales)]
    public class DealsController : Controller
    {
        private Model db = new Model();
        private IEnumerable<Customer> Customers { get { return  db.Customers.OrderByDescending(x => x.CreationDate).ToList();} }
        
        private SelectList GetCustomersSelectList(int? selectedId= null)
        {
            var res = new SelectList(
                Customers.OrderByDescending(x => x.Deals.Count)
                    .ThenBy(x => x.Name)
                    .Select(x => new SelectListItem()
                    {Text = $"{x.Name} ({x.CreationDate.ToString("dd.MM.yyyy")})", Value = x.CustomerId.ToString()}),
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            return View(deal);
        }

        // GET: Deals/Create
        public ActionResult Create(int? customerId)
        {
            ViewBag.CustomerId = GetCustomersSelectList(customerId);
            ViewBag.DealStateId = new SelectList(db.DealStates.OrderBy(x => x.Order), nameof(DealState.DealStateId), nameof(DealState.Name));
            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes.OrderBy(x => x.ServiceTypeId), nameof(ServiceType.ServiceTypeId), nameof(ServiceType.Name));
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
        public ActionResult Create([Bind(Include = "DealId,CreationDate,Sum,Profit,CustomerId,ServiceTypeId,Name,Description,Type,DealStateId,NeedsAttention")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                deal.Creator = User.Identity.Name;
                db.Deals.Add(deal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = GetCustomersSelectList();
            ViewBag.DealStateId = new SelectList(db.DealStates.OrderBy(x => x.Order), nameof(DealState.DealStateId), nameof(DealState.Name));
            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes.OrderBy(x => x.ServiceTypeId), nameof(ServiceType.ServiceTypeId), nameof(ServiceType.Name));
            return View(deal);
        }

        // GET: Deals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = GetCustomersSelectList(deal.CustomerId);
            ViewBag.DealStateId = new SelectList(db.DealStates.OrderBy(x => x.Order), nameof(DealState.DealStateId), nameof(DealState.Name), deal.DealStateId);
            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes.OrderBy(x => x.ServiceTypeId), nameof(ServiceType.ServiceTypeId), nameof(ServiceType.Name), deal.ServiceTypeId);
            return View(deal);
        }

        // POST: Deals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DealId,Creator,CreationDate,ServiceTypeId,Sum,Profit,CustomerId,Name,Description,Type,DealStateId,NeedsAttention")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Edit(deal.DealId);
        }

        // GET: Deals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            return View(deal);
        }

        // POST: Deals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deal deal = db.Deals.Find(id);
            db.Deals.Remove(deal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
