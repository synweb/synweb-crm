namespace SynWebCRM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Deal")]
    public partial class Deal
    {
        public int DealId { get; set; }

        [Display(Name="Дата создания", AutoGenerateField = true)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        [Display(Name="Сумма")]
        public decimal? Sum { get; set; }

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

        [ForeignKey("DealStateId")]
        [Display(Name = "Состояние")]
        public virtual DealState DealState { get; set; }

        [Display(Name = "Клиент")]
        public virtual Customer Customer { get; set; }
        [Display(Name = "Создатель")]
        public string Creator { get; set; }
    }
}
