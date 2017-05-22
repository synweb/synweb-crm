using System;
using System.Reflection;
using SynWebCRM.Contract.Repositories;

namespace SynWebCRM.Data.EF
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
