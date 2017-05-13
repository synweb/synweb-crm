namespace SynWebCRM.Web.Models
{
    public class MonthlyFunnel
    {
        public MonthlyFunnel(int monthOrder, string month)
        {
            Month = month;
            MonthOrder = monthOrder;
        }

        public int MonthOrder { get; private set; }
        public string Month { get; private set; }
        public int NewCustomers { get; set; }
        public int NewIncomingRequests { get; set; }
        public int CompletedDeals { get; set; }
        public int NewOutcomingRequests { get; set; }
    }
}