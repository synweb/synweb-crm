using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SynWebCRM.Contract.Models
{
    [Table("DealState")]
    public partial class DealState
    {
        public DealState()
        {
            Deals = new HashSet<Deal>();
        }
        public int DealStateId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public bool IsCompleted { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Deal> Deals { get; set; }
    }
}
