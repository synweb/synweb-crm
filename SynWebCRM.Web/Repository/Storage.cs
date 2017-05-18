using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SynWebCRM.Web.Data;

namespace SynWebCRM.Web.Repository
{
    public class Storage
    {
        public CRMModel StorageContext { get; private set; }

        public Storage(CRMModel model)
        {
            this.StorageContext = model;
        }

        public T GetRepository<T>() where T : IRepository
        {
            foreach (Type type in this.GetType().GetTypeInfo().Assembly.GetTypes())
            {
                if (typeof(T).GetTypeInfo().IsAssignableFrom(type) && type.GetTypeInfo().IsClass)
                {
                    T repository = (T)Activator.CreateInstance(type);

                    repository.SetStorageContext(this.StorageContext);
                    return repository;
                }
            }

            return default(T);
        }

        public void Save()
        {
            // Do nothing
        }
    }
}
