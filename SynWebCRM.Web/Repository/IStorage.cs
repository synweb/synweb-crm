using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynWebCRM.Web.Repository
{
    public interface IStorage
    {
        T GetRepository<T>() where T : IRepository;
        void Save();
    }
}
