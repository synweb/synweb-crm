using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SynWebCRM.Web.Repository
{
    public interface IRepository
    {
        void SetStorageContext(IStorageContext storageContext);
    }

    public interface IRepository<TEntity, TKey> : IRepository where TEntity : class
    {
        IQueryable<TEntity> All();
        IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetById(TKey id);
        TKey Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TKey id);
    }
}
