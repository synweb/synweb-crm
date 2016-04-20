using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SynWebCRM.Data;
using SynWebCRM.Security;

namespace SynWebCRM.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.Sales)]
    public class EstimatesController:Controller
    {
        private readonly Model db = new Model();

        public ActionResult Create(int id)
        {
            var model = new Estimate
            {
                Guid = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Creator = User.Identity.Name,
                DealId = id
            };
            db.Estimates.Add(model);
            db.SaveChanges();
            return RedirectToAction("Edit", new {id=model.EstimateId});
        }

        public ActionResult Edit(int id)
        {

            var model = db.Estimates.Find(id);
            var deal = db.Deals.Find(model.DealId);
            ViewBag.Deal = deal;
            return View(model);
        }

        // POST: Deals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstimateId,CreationDate,Guid,DealId,Creator,Discount,Total")] Estimate estimate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estimate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("View", new {id=estimate.Guid});
            }

            return View(estimate);
        }

        [AllowAnonymous]
        public ActionResult View(Guid id)
        {
            var estimate = db.Estimates.SingleOrDefault(x => x.Guid == id);
            return View(estimate);
        }
    }
}