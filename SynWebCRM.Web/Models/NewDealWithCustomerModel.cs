using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SynWebCRM.Contract.Models;

namespace SynWebCRM.Web.Models
{
    public class NewDealWithCustomerModel
    {
        public Customer Customer { get; set; }
        public Deal Deal { get; set; }
    }
}
