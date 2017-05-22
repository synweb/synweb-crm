using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SynWebCRM.Contract.Repositories
{
    public interface IRepository
    {
        void SetStorageContext(IStorageContext storageContext);
    }

    public interface IRepository<TEntity, TKey> : IRepository where TEntity : class
    {
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> AllIncluding<TProp>(params Expression<Func<TEntity, TProp>>[] includeProperties);
        TEntity GetById(TKey id);
        TKey Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TKey id);
    }
}
