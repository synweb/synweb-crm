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
    [Authorize(Roles = CRMRoles.Admin)]
    public class WebsitesController : Controller
    {
        private Model db = new Model();

        // GET: Websites
        public ActionResult Index()
        {
            var websites = db.Websites.Include(w => w.Customer).ToList()
                .OrderByDescending(x => x.IsActive)
                .ThenByDescending(x => x.HostingEndingDate.HasValue || x.DomainEndingDate.HasValue)
                .ThenBy(x =>
                {
                    if (x.HostingEndingDate.HasValue)
                    {
                        return x.DomainEndingDate.HasValue
                            ? new DateTime(Math.Min(x.HostingEndingDate.Value.Ticks, x.DomainEndingDate.Value.Ticks))
                            : x.HostingEndingDate.Value;
                    }
                    return x.DomainEndingDate ?? new DateTime();
                });
            return View(websites);
        }

        // GET: Websites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website website = db.Websites.Find(id);
            if (website == null)
            {
                return HttpNotFound();
            }
            return View(website);
        }

        // GET: Websites/Create
        public ActionResult Create()
        {
            ViewBag.OwnerId = new SelectList(db.Customers, "CustomerId", "Name");
            var model = new Website
            {
                HostingEndingDate = DateTime.Now.AddYears(1),
                IsActive = true
            };
            return View(model);
        }

        // POST: Websites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WebsiteId,CreationDate,OwnerId,Domain,DomainEndingDate,HostingEndingDate,HostingPrice,IsActive")] Website website)
        {
            if (ModelState.IsValid)
            {
                website.CreationDate = DateTime.Now;
                db.Websites.Add(website);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerId = new SelectList(db.Customers, "CustomerId", "Name", website.OwnerId);
            return View(website);
        }

        // GET: Websites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website website = db.Websites.Find(id);
            if (website == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerId = new SelectList(db.Customers, "CustomerId", "Name", website.OwnerId);
            return View(website);
        }

        // POST: Websites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WebsiteId,CreationDate,OwnerId,Domain,DomainEndingDate,HostingEndingDate,HostingPrice,IsActive")] Website website)
        {
            if (ModelState.IsValid)
            {
                db.Entry(website).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(db.Customers, "CustomerId", "Name", website.OwnerId);
            return View(website);
        }

        // GET: Websites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website website = db.Websites.Find(id);
            if (website == null)
            {
                return HttpNotFound();
            }
            return View(website);
        }

        // POST: Websites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Website website = db.Websites.Find(id);
            db.Websites.Remove(website);
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
