using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynWebCRM.Data
{
    [Table("Event")]
    public class Event
    {
        public int EventId { get; set; }

        [Display(Name = "Дата добавления")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }


        [Display(Name = "Начало")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }


        [Display(Name = "Окончание")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        
        [Required]
        [StringLength(200)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        //[StringLength(500)]
        //[Display(Name = "Url")]
        //public string Url { get; set; }
    }
}
