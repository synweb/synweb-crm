using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SynWebCRM.Web.ApiControllers.Models;
using SynWebCRM.Web.Data;
using SynWebCRM.Web.Models;
using SynWebCRM.Web.Security;

namespace SynWebCRM.Web.ApiControllers
{
    [Authorize(Roles = CRMRoles.Admin + "," + CRMRoles.Sales)]
    public class EstimatesController : Controller
    {
        private CRMModel _crmModel;

        public EstimatesController(CRMModel crmModel)
        {
            _crmModel = crmModel;
        }
        
        [HttpGet]
        [Route("api/estimates/{id}/get")]
        public ResultModel GetEstimate(int id)
        {
            try
            {
                
                var rec = _crmModel.Estimates
                    .Include(x => x.Items)
                    .SingleOrDefault(x => x.EstimateId == id);
                if (rec == null)
                {
                    return new ResultModel(false, $"id {id} NotFound");
                }
                rec.Items = rec.Items.OrderBy(x => x.SortOrder).ThenBy(x => x.CreationDate).ToList();
                return new ResultModel(true, rec);
            }
            catch (Exception e)
            {
                return new ResultModel(e);
            }

        }

        [HttpPost]
        [Route("/api/estimates/update")]
        public ResultModel UpdateEstimate(Estimate estimate)
        {

            try
            {
                estimate.Items = estimate.Items.Where(x => !string.IsNullOrEmpty(x.Name)).ToList();

                foreach (var item in estimate.Items)
                {
                    if (item.EstimateId == 0)
                    {
                        item.EstimateId = estimate.EstimateId;
                        item.CreationDate = DateTime.Now;
                        _crmModel.EstimateItems.Add(item);
                    }
                    else
                    {
                        _crmModel.EstimateItems.Attach(item);
                        _crmModel.Entry(item).State = EntityState.Modified;
                    }
                }
                var itemsForDelete =
                    _crmModel.EstimateItems.Where(x => x.EstimateId == estimate.EstimateId).ToList()
                        .Where(x => estimate.Items.All(y => y.ItemId != x.ItemId)).ToList();
                foreach (var item in itemsForDelete)
                {
                    _crmModel.EstimateItems.Remove(item);
                }

                _crmModel.Estimates.Attach(estimate);
                _crmModel.Entry(estimate).State = EntityState.Modified;
                _crmModel.SaveChanges();
                return new ResultModel(true);
            }
            catch (Exception e)
            {
                return new ResultModel(e);
            }

        }
    }
}