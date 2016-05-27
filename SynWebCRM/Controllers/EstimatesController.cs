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
    [Authorize(Roles = CRMRoles.Admin + "," + CRMRoles.Sales)]
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
                DealId = id,
                HourlyRate = 1500 //TODO: в конфиг
            };
            db.Estimates.Add(model);
            db.SaveChanges();
            return RedirectToAction("Edit", new {id=model.EstimateId});
        }

        public ActionResult Edit(int id)
        {

            var model = db.Estimates.Find(id);
            model.Items = model.Items.OrderBy(x => x.SortOrder).ToList();
            var deal = db.Deals.Find(model.DealId);
            ViewBag.Deal = deal;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult View(Guid id)
        {
            var estimate = db.Estimates.Single(x => x.Guid == id);
            estimate.Items = estimate.Items.OrderBy(x => x.SortOrder).ToList();
            return View(estimate);
        }
    }
}