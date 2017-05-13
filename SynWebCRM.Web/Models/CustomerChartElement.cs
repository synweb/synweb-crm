using SynWebCRM.Web.Data;

namespace SynWebCRM.Web.Models
{
    public class CustomerChartElement
    {
        public Customer Customer { get; set; }

        /// <summary>
        /// Average period between orders, days
        /// </summary>
        public double Period { get; set; }

        /// <summary>
        /// Total Customer summary
        /// </summary>
        public decimal Summary { get; set; }

        /// <summary>
        /// Days after last order
        /// </summary>
        public double Recency { get; set; }
        
        /// <summary>
        /// Completed order count
        /// </summary>

        public int OrderCount { get; set; }

        /// <summary>
        /// Customer rating
        /// </summary>
        public ClientRating Rating { get; set; }

        /// <summary>
        /// Numeric value of customer rating
        /// </summary>
        public double RatingValue { get; set; }
    }
}