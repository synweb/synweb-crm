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
    [Authorize(Roles = Roles.Admin + "," + Roles.Sales)]
    public class DealsController : Controller
    {
        private Model db = new Model();
        private IEnumerable<Customer> Customers { get { return  db.Customers.OrderByDescending(x => x.CreationDate).ToList();} }
        
        private SelectList CustomersSelectList
        {
            get
            {
                return new SelectList(
                    Customers.Select(x => new SelectListItem()
                        {Text = $"{x.Name} ({x.CreationDate.ToString("dd.MM.yyyy")})", Value = x.CustomerId.ToString()}).OrderByDescending(x => x.Value),
                "Value", "Text"
                );
            }
        }

        // GET: Deals
        public ActionResult Index()
        {
            var deals = db.Deals
                .Include(x => x.DealState)
                .Include(d => d.Customer).OrderByDescending(x => x.CreationDate);
            return View(deals);
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
            if (customerId.HasValue)
            {
                ViewBag.CustomerId = new SelectList(
                    Customers.Select(x => new SelectListItem()
                    { Text = $"{x.Name} ({x.CreationDate.ToString("dd.MM.yyyy")})", Value = x.CustomerId.ToString() }).OrderByDescending(x => x.Value),
                "Value", "Text", customerId.Value);
            }
            else
            {
                ViewBag.CustomerId = CustomersSelectList;
            }
            ViewBag.DealStateId = new SelectList(db.DealStates.OrderBy(x => x.Order), nameof(DealState.DealStateId), nameof(DealState.Name));
            return View();
        }

        // POST: Deals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DealId,Sum,CustomerId,Name,Description,Type,DealStateId")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                deal.CreationDate = DateTime.Now;
                deal.Creator = User.Identity.Name;
                db.Deals.Add(deal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = CustomersSelectList;
            ViewBag.DealStateId = new SelectList(db.DealStates.OrderBy(x => x.Order), nameof(DealState.DealStateId), nameof(DealState.Name));
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
            ViewBag.CustomerId = new SelectList(
                Customers.Select(x => new SelectListItem()
                { Text = $"{x.Name} ({x.CreationDate.ToString("dd.MM.yyyy")})", Value = x.CustomerId.ToString() }).OrderByDescending(x => x.Value),
            "Value", "Text", deal.CustomerId);

            ViewBag.DealStateId = new SelectList(db.DealStates.OrderBy(x => x.Order), nameof(DealState.DealStateId), nameof(DealState.Name), deal.DealStateId);
            return View(deal);
        }

        // POST: Deals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DealId,CreationDate,Sum,CustomerId,Name,Description,Type,DealStateId")] Deal deal)
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
