using System.ComponentModel.DataAnnotations;

namespace SynWebCRM.DataMigrator.Models
{
    public enum CustomerSource
    {
        [Display(Name = "ВК")]
        Vk = 1,
        [Display(Name = "Почта")]
        Email = 2,
        [Display(Name = "Звонок")]
        Phone = 3,
        [Display(Name = "Сарафан")]
        Recommendation = 4,
        [Display(Name = "Друг")]
        Friend = 5,
        [Display(Name = "Заявка на сайте")]
        Site = 6,
        [Display(Name = "Другой")]
        Other = 7

    }
}
