using System;
using System.Collections.Generic;
using System.Text;
using SynWebCRM.Contract.Models;

namespace SynWebCRM.Contract.Repositories
{
    public interface ICustomerRepository: IRepository<Customer, int>
    {
        IEnumerable<Customer> AllWithDeals();
    }
}
