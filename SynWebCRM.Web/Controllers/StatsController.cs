using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SynWebCRM.Web.Data;
using SynWebCRM.Web.Models;

namespace SynWebCRM.Web.Controllers
{
    public class StatsController: Controller
    {
        public StatsController(CRMModel crmModel)
        {
            _crmModel = crmModel;
        }

        private readonly CRMModel _crmModel;
        private readonly DateTime _startDate = DateTime.Now.AddMonths(-3);

        public ActionResult Index()
        {
            var model = new IndexStatsModel();
            model.RevenueByService = GetRevenueByService();
            model.RevenueByCustomer = GetRevenueByCustomer();
            return View(model);
        }

        private IDictionary<Customer, decimal> GetRevenueByCustomer()
        {
            var groups = LatestCompletedDeals.GroupBy(x => x.Customer);
            return groups.ToDictionary(g => g.Key, g => g.Sum(x => x.Profit.Value));
        }

        private IDictionary<ServiceType, decimal> GetRevenueByService()
        {
            var groups = LatestCompletedDeals.GroupBy(x => x.ServiceType);
            return groups.ToDictionary(g => g.Key, g => g.Sum(x => x.Profit.Value));
        }

        private ICollection<Deal> LatestCompletedDeals => _crmModel.Deals.Where(x => x.DealState.IsCompleted && x.Profit.HasValue && x.CreationDate > _startDate).ToList();
    }
}