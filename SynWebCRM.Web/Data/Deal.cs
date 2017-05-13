namespace SynWebCRM.Web.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Deal")]
    public partial class Deal
    {
        public Deal()
        {
            Notes = new HashSet<Note>();
            Estimates = new HashSet<Estimate>();
        }
        public int DealId { get; set; }

        [Display(Name="Дата создания", AutoGenerateField = true)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        [Display(Name="Сумма")]
        public decimal? Sum { get; set; }

        [Display(Name = "Прибыль")]
        public decimal? Profit { get; set; }

        [Display(Name="Клиент")]
        public int CustomerId { get; set; }

        [StringLength(200)]
        [Display(Name="Название")]
        public string Name { get; set; }

        [Display(Name="Описание")]
        public string Description { get; set; }
        
        [Display(Name = "Требует внимания")]
        public bool NeedsAttention { get; set; }

        [Required]
        [EnumDataType(typeof(DealType))]
        [Display(Name="Тип")]
        public DealType Type { get; set; }

        [Required]
        [Display(Name = "Состояние")]
        public int DealStateId { get; set; }

        [Required]
        [Display(Name = "Услуга")]
        public int? ServiceTypeId { get; set; }

        [ForeignKey("DealStateId")]
        [Display(Name = "Состояние")]
        public virtual DealState DealState { get; set; }

        [Display(Name = "Клиент")]
        public virtual Customer Customer { get; set; }
        [Display(Name = "Создатель")]
        public string Creator { get; set; }

        [ForeignKey("ServiceTypeId")]
        [Display(Name = "Услуга")]
        public virtual ServiceType ServiceType { get; set; }
        public virtual ICollection<Note> Notes { get; set; }

        [Display(Name = "Сметы")]
        public virtual ICollection<Estimate> Estimates { get; set; }
    }
}
