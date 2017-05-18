using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SynWebCRM.Contract.Models
{
    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Deals = new HashSet<Deal>();
            Websites = new HashSet<Website>();
            Notes = new HashSet<Note>();
        }

        [Display(Name = "Клиент")]
        public int CustomerId { get; set; }

        [Display(Name="Дата добавления")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name="Имя")]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(CustomerSource))]
        [Display(Name="Первый источник")]
        public CustomerSource Source { get; set; }

        [Display(Name="Описание")]
        public string Description { get; set; }

        [StringLength(100)]
        [Display(Name="Телефон")]
        public string Phone { get; set; }

        [StringLength(100)]
        [EmailAddress]
        [Display(Name="Почта")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name="ID в VK")]
        public string VkId { get; set; }

        [Display(Name="Требует внимания")]
        public bool NeedsAttention { get; set; }

        public virtual ICollection<Deal> Deals { get; set; }

        public virtual ICollection<Website> Websites { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        [Display(Name = "Создатель")]
        public string Creator { get; set; }
    }
}
