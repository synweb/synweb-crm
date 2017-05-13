using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SynWebCRM.Web.Data;
using SynWebCRM.Web.Security;

namespace SynWebCRM.Web.Controllers
{
    [Authorize(Roles = CRMRoles.Admin + "," + CRMRoles.Sales)]
    public class EstimatesController : Controller
    {
        public EstimatesController(CRMModel crmModel)
        {
            _crmModel = crmModel;
        }

        private readonly CRMModel _crmModel;

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
            _crmModel.Estimates.Add(model);
            _crmModel.SaveChanges();
            return RedirectToAction("Edit", new { id = model.EstimateId });
        }

        public ActionResult Edit(int id)
        {
            var model = _crmModel.Estimates
                .Include(x => x.Deal)
                .SingleOrDefault(x => x.EstimateId == id);
            if(model == null)
                return StatusCode(404);
            ViewBag.Deal = model.Deal;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult View(Guid id)
        {
            var estimate = _crmModel.Estimates.Include(x => x.Items).Single(x => x.Guid == id);
            estimate.Items = estimate.Items.OrderBy(x => x.SortOrder).ThenBy(x => x.CreationDate).ToList();
            return View(estimate);
        }
    }
}