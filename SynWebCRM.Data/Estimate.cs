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
        public int Discount { get; set; }

        [Display(Name = "Итог без скидки")]
        public decimal Total { get; set; }

        [Display(Name = "Итог в месяц без скидки")]
        public decimal MonthlyTotal { get; set; }

        [Display(Name = "Часовая ставка")]
        public decimal HourlyRate { get; set; }

        public virtual Deal Deal { get; set; }
        [Display(Name = "Создатель")]
        public string Creator { get; set; }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Text { get; set; }
        public bool RequisitesVisible { get; set; }

        [Display(Name = "Пункты")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstimateItem> Items { get; set; } 
    }
}
