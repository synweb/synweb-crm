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
        public Estimate()
        {
            Items = new HashSet<EstimateItem>();
        }

        public int EstimateId { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "GUID")]
        public Guid Guid { get; set; }

        public int DealId { get; set; }

        [Display(Name = "Скидка")]
        public int? Discount { get; set; }

        [Display(Name = "Итог без скидки")]
        public decimal Total { get; set; }

        public virtual Deal Deal { get; set; }
        [Display(Name = "Создатель")]
        public string Creator { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstimateItem> Items { get; set; } 
    }
}
