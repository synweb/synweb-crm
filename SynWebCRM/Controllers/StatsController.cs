using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SynWebCRM.Data;
using SynWebCRM.Models;

namespace SynWebCRM.Controllers
{
    public class StatsController: Controller
    {
        private Model db = new Model();

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

        private ICollection<Deal> LatestCompletedDeals => db.Deals.Where(x => x.DealState.IsCompleted && x.Profit.HasValue && x.CreationDate > _startDate).ToList();
    }
}