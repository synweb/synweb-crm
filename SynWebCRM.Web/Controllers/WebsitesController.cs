using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SynWebCRM.Web.Data;
using SynWebCRM.Web.Security;

namespace SynWebCRM.Web.Controllers
{
    [Authorize(Roles = CRMRoles.Admin)]
    public class WebsitesController : Controller
    {
        private CRMModel _crmModel;

        public WebsitesController(CRMModel crmModel)
        {
            _crmModel = crmModel;
        }

        // GET: Websites
        public ActionResult Index()
        {
            var websites = _crmModel.Websites.Include(w => w.Customer).ToList()
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
                return StatusCode(400);
            }
            Website website = _crmModel.Websites.Include(x => x.Customer).SingleOrDefault(x => x.WebsiteId == id);
            if (website == null)
            {
                return StatusCode(400);
            }
            return View(website);
        }

        // GET: Websites/Create
        public ActionResult Create()
        {
            ViewBag.OwnerId = new SelectList(_crmModel.Customers, "CustomerId", "Name");
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
        public ActionResult Create([Bind("WebsiteId,CreationDate,OwnerId,Domain,DomainEndingDate,HostingEndingDate,HostingPrice,IsActive")] Website website)
        {
            if (ModelState.IsValid)
            {
                website.CreationDate = DateTime.Now;
                _crmModel.Websites.Add(website);
                _crmModel.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerId = new SelectList(_crmModel.Customers, "CustomerId", "Name", website.OwnerId);
            return View(website);
        }

        // GET: Websites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);
            }
            Website website = _crmModel.Websites.Find(id);
            if (website == null)
            {
                return StatusCode(400);
            }
            ViewBag.OwnerId = new SelectList(_crmModel.Customers, "CustomerId", "Name", website.OwnerId);
            return View(website);
        }

        // POST: Websites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("WebsiteId,CreationDate,OwnerId,Domain,DomainEndingDate,HostingEndingDate,HostingPrice,IsActive")] Website website)
        {
            if (ModelState.IsValid)
            {
                _crmModel.Entry(website).State = EntityState.Modified;
                _crmModel.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(_crmModel.Customers, "CustomerId", "Name", website.OwnerId);
            return View(website);
        }

        // GET: Websites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);
            }
            Website website = _crmModel.Websites.Find(id);
            if (website == null)
            {
                return StatusCode(400);
            }
            return View(website);
        }

        // POST: Websites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Website website = _crmModel.Websites.Find(id);
            _crmModel.Websites.Remove(website);
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
