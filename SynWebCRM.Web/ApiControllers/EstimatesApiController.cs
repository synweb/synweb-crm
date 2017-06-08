using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SynWebCRM.Web.Models;
using SynWebCRM.Web.Security;
using SynWebCRM.Data.EF;
using SynWebCRM.Contract.Models;
using SynWebCRM.Contract.Repositories;

namespace SynWebCRM.Web.ApiControllers
{
    [Authorize(Roles = CRMRoles.Admin + "," + CRMRoles.Sales)]
    public class EstimatesApiController : Controller
    {
        private readonly IEstimateRepository _estimateRepository;
        private readonly IEstimateItemRepository _estimateItemRepository;

        public EstimatesApiController(IEstimateRepository estimateRepository, IEstimateItemRepository estimateItemRepository)
        {
            _estimateRepository = estimateRepository;
            _estimateItemRepository = estimateItemRepository;
        }

        [HttpGet]
        [Route("api/estimates/{id}/get")]
        public ResultModel GetEstimate(int id)
        {
            try
            {

                var rec = _estimateRepository.GetById(id);
                if (rec == null)
                {
                    return new ResultModel(false, $"id {id} NotFound");
                }
                rec.Items = _estimateItemRepository.GetByEstimateId(id).ToList(); //rec.Items.OrderBy(x => x.SortOrder).ThenBy(x => x.CreationDate).ToList();
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
                        _estimateItemRepository.Add(item);
                    }
                    else
                    {
                        _estimateItemRepository.Update(item);
                    }
                }
                var itemsForDelete =
                    _estimateItemRepository.GetByEstimateId(estimate.EstimateId)
                        .Where(x => estimate.Items.All(y => y.ItemId != x.ItemId)).ToList();
                foreach (var item in itemsForDelete)
                {
                    _estimateItemRepository.Delete(item);
                }

                _estimateRepository.Update(estimate);
                return new ResultModel(true);
            }
            catch (Exception e)
            {
                return new ResultModel(e);
            }

        }
    }
}