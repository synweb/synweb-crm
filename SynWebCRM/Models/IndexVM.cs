using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SynWebCRM.Models
{
    public class IndexVM
    {
        public IndexVM()
        {
            Funnel = new List<MonthlyFunnel>();
        }

        public ICollection<MonthlyFunnel> Funnel { get; set; }

        public int Customers => Funnel.Sum(x => x.NewCustomers);
        public int IncomingRequests => Funnel.Sum(x => x.NewIncomingRequests);
        public int CompletedDeals => Funnel.Sum(x => x.CompletedDeals);
        public int OutcomingRequests => Funnel.Sum(x => x.NewOutcomingRequests);
    }
}