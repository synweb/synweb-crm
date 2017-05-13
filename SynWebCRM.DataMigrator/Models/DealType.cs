using System.ComponentModel.DataAnnotations;

namespace SynWebCRM.DataMigrator.Models
{
    public enum DealType
    {
        [Display(Name="Входящая")]
        Incoming = 1,
        [Display(Name="Исходящая")]
        Outcoming = 2
    }
}
