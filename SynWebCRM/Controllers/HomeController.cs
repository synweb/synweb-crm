using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SynWebCRM.Data;
using SynWebCRM.Models;

namespace SynWebCRM.Controllers
{
    public class HomeController : Controller
    {
        private Model db = new Model();

        private Dictionary<int, string> _months = new Dictionary<int, string>()
        {
            {1, "Январь"},
            {2, "Февраль"},
            {3, "Март"},
            {4, "Апрель"},
            {5, "Май"},
            {6, "Июнь"},
            {7, "Июль"},
            {8, "Август"},
            {9, "Сентябрь"},
            {10, "Октябрь"},
            {11, "Ноябрь"},
            {12, "Декабрь"}
        };

        public ActionResult Index()
        {
            var now = DateTime.Now;
            var vm = new IndexVM();
            ICollection<MonthlyFunnel> funnel = _months.Select(x => new MonthlyFunnel(x.Key, x.Value)).ToList();
            foreach (var month in funnel)
            {
                month.NewCustomers = db.Customers.Count(x => x.CreationDate.Year == now.Year 
                    && x.CreationDate.Month == month.MonthOrder);
                month.NewIncomingRequests = db.Deals.Count(x => x.CreationDate.Year == now.Year
                    && x.Type==DealType.Incoming
                    && x.CreationDate.Month == month.MonthOrder);
                month.NewOutcomingRequests = db.Deals.Count(x => x.CreationDate.Year == now.Year
                    && x.Type == DealType.Outcoming
                    && x.CreationDate.Month == month.MonthOrder);
                month.CompletedDeals = db.Deals.Count(x => x.CreationDate.Year == now.Year
                    && x.CreationDate.Month == month.MonthOrder
                    && x.DealState.IsCompleted);
            }
            vm.Funnel = funnel;
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}