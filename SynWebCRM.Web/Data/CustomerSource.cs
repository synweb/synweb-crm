using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynWebCRM.Web.Data
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
