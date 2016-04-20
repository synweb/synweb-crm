namespace SynWebCRM.ApiControllers.Models
{
    public class DealModel
    {
        public int DealId { get; set; }

        public string CreationDate { get; set; }

        public decimal? Sum { get; set; }

        public CustomerModel Customer { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string DealState { get; set; }
        public bool NeedsAttention { get; set; }
    }
}