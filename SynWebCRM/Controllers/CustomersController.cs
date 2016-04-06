using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using SynWebCRM.Data;
using SynWebCRM.Helpers;
using SynWebCRM.Security;

namespace SynWebCRM.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.Sales)]
    public class CustomersController : Controller
    {
        private Model db = new Model();

        private IQueryable<Customer> Customers => db.Customers.OrderByDescending(x => x.CreationDate);

        // GET: Customers
        public ActionResult Index()
        {
            return View(Customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ICollection<Deal> deals = customer.Deals;//db.Deals.Where(x => x.CustomerId == id).ToList();
            ViewBag.Deals = deals;
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,Name,Source,Description,Phone,Email,VkId")] Customer customer)
        {
            if (!string.IsNullOrEmpty(customer.VkId))
            {
                customer.VkId = ParseHelper.ParseVK(customer.VkId);
            }
            if (ModelState.IsValid)
            {
                customer.CreationDate = DateTime.Now;
                customer.Creator = User.Identity.Name;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Details", new {id = customer.CustomerId});
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,CreationDate,Name,Source,Description,Phone,Email,VkId")] Customer customer)
        {
            if (!string.IsNullOrEmpty(customer.VkId))
            {
                customer.VkId = ParseHelper.ParseVK(customer.VkId);
            }
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
