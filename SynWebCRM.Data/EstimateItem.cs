namespace SynWebCRM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EstimateItem")]
    public partial class EstimateItem
    {
        [Key]
        public int ItemId { get; set; }

        public DateTime CreationDate { get; set; }

        public int EstimateId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public double? DevelopmentHours { get; set; }
        public bool PerMonth { get; set; }

        public bool IsOptional { get; set; }

        public int SortOrder { get; set; }

        //public Estimate Estimate { get; set; }
    }
}
