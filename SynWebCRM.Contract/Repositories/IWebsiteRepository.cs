using System;
using System.Collections.Generic;
using System.Text;
using SynWebCRM.Contract.Models;

namespace SynWebCRM.Contract.Repositories
{
    public interface IWebsiteRepository: IRepository<Website, int>
    {
        ICollection<Website> GetEndingByDomain(DateTime start, DateTime end);
        ICollection<Website> GetEndingByHosting(DateTime start, DateTime end);
    }
}
