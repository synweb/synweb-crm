using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynWebCRM.Web.Data
{
    [Table("ServiceType")]
    public class ServiceType
    {
        public ServiceType()
        {
            Deals = new HashSet<Deal>();
        }
        public int ServiceTypeId { get; set; }
        public string Name { get; set; }
        public ICollection<Deal> Deals { get; set; }
    }
}
