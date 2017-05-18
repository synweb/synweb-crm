using System.ComponentModel.DataAnnotations;

namespace SynWebCRM.Contract.Models
{
    public enum DealType
    {
        [Display(Name="Входящая")]
        Incoming = 1,
        [Display(Name="Исходящая")]
        Outcoming = 2
    }
}
