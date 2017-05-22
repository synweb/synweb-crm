using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SynWebCRM.Data.EF
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        internal CRMModel _db;
        internal readonly DbSet<TEntity> _dbSet;

        public BaseRepository(CRMModel context)
        {
            this._db = context;
            this._dbSet = context.Set<TEntity>();
        }

        protected virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params string[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        private static readonly char[] _includesSeparators = new[] {','};

        protected virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            return Get(filter, orderBy,
                includeProperties.Split(_includesSeparators, StringSplitOptions.RemoveEmptyEntries));
        }

        protected virtual TEntity GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        protected virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        protected virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        protected virtual void Delete(TEntity entityToDelete)
        {
            if (_db.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        protected virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _db.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }

}
