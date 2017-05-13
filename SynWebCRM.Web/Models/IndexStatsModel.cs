using System.Collections.Generic;
using SynWebCRM.Web.Data;

namespace SynWebCRM.Web.Models
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