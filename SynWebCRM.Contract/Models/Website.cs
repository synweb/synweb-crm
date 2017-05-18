using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SynWebCRM.Contract.Models
{
    [Table("Website")]
    public partial class Website
    {
        public int WebsiteId { get; set; }

        [Display(Name = "Дата добавления")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Клиент")]
        public int OwnerId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Домен")]
        public string Domain { get; set; }

        [Display(Name = "Окончание хостинга")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? HostingEndingDate { get; set; }

        [Display(Name = "Окончание домена")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DomainEndingDate { get; set; }

        [Display(Name = "Обслуживание")]
        public decimal? HostingPrice { get; set; }

        [Display(Name = "Клиент")]
        public virtual Customer Customer { get; set; }

        [Display(Name = "Создатель")]
        public string Creator { get; set; }

        [Display(Name = "Обслуживается")]
        public bool IsActive { get; set; }
    }
}
