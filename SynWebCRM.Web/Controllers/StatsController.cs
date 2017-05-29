using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SynWebCRM.Web.Models;
using SynWebCRM.Data.EF;
using SynWebCRM.Contract.Models;
using SynWebCRM.Contract.Repositories;

namespace SynWebCRM.Web.Controllers
{
    public class StatsController: Controller
    {
        public StatsController(IDealRepository dealRepository)
        {
            _dealRepository = dealRepository;
        }


        private readonly IDealRepository _dealRepository;
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

        private IEnumerable<Deal> LatestCompletedDeals => _dealRepository.GetLatestCompletedDeals(_startDate); 
    }
}