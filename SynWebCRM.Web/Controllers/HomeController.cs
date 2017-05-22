using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SynWebCRM.Web.Models;
using SynWebCRM.Data.EF;
using SynWebCRM.Contract.Models;

namespace SynWebCRM.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(CRMModel crmModel)
        {
            _crmModel = crmModel;
        }

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
        private readonly CRMModel _crmModel;

        public ActionResult Index()
        {
            var now = DateTime.Now;
            var vm = new IndexVM();
            ICollection<MonthlyFunnel> funnel = _months.Select(x => new MonthlyFunnel(x.Key, x.Value)).ToList();
            foreach (var month in funnel)
            {
                month.NewCustomers = _crmModel.Customers.Count(x => x.CreationDate.Year == now.Year
                    && x.CreationDate.Month == month.MonthOrder);
                month.NewIncomingRequests = _crmModel.Deals.Count(x => x.CreationDate.Year == now.Year
                    && x.Type == DealType.Incoming
                    && x.CreationDate.Month == month.MonthOrder);
                month.NewOutcomingRequests = _crmModel.Deals.Count(x => x.CreationDate.Year == now.Year
                    && x.Type == DealType.Outcoming
                    && x.CreationDate.Month == month.MonthOrder);
                month.CompletedDeals = _crmModel.Deals.Count(x => x.CreationDate.Year == now.Year
                    && x.CreationDate.Month == month.MonthOrder
                    && x.DealState.IsCompleted);
            }
            vm.Funnel = funnel;
            return View(vm);
           // return View(new IndexVM());
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