using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SynWebCRM.DataMigrator.Models
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
