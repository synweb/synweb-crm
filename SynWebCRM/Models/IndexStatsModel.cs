using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SynWebCRM.Data;

namespace SynWebCRM.Models
{
    public class IndexStatsModel
    {
        public IndexStatsModel()
        {
            RevenueByService = new Dictionary<ServiceType, decimal>();
        }
        public IDictionary<ServiceType, decimal> RevenueByService { get; set; }
        public IDictionary<Customer, decimal> RevenueByCustomer { get; set; }
    }
}