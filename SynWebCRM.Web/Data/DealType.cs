using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynWebCRM.Web.Data
{
    public enum DealType
    {
        [Display(Name="Входящая")]
        Incoming = 1,
        [Display(Name="Исходящая")]
        Outcoming = 2
    }
}
