namespace SynWebCRM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Estimate")]
    public partial class Estimate
    {
        public int EstimateId { get; set; }

        public DateTime CreationDate { get; set; }

        public int CustomerId { get; set; }

        public int? Discount { get; set; }

        public decimal Total { get; set; }

        public virtual Customer Customer { get; set; }
        [Display(Name = "Создатель")]
        public string Creator { get; set; }
    }
}
