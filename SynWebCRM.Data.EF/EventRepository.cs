using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SynWebCRM.Contract.Models;
using SynWebCRM.Contract.Repositories;

namespace SynWebCRM.Data.EF
{
    public class EventRepository: IEventRepository
    {
        private CRMModel _db;

        public EventRepository(CRMModel db)
        {
            _db = db;
        }

        #region Dispose

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public IEnumerable<Event> All()
        {
            return _db.Events;
        }

        public Event GetById(int id)
        {
            return _db.Events.Find(id);
        }

        public void Update(Event entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(Event entity)
        {
            _db.Events.Remove(entity);
        }

        public void Delete(int id)
        {
            _db.Events.Remove(_db.Events.Find(id));
        }

        public ICollection<Event> GetByDates(DateTime start, DateTime end)
        {
            return _db.Events.Where(x => x.StartDate >= start
                                         && x.StartDate <= end
                                         || x.EndDate.HasValue
                                         && x.EndDate >= start
                                         && x.EndDate <= end)
                    .ToList();
        }

        public void SetStorageContext(IStorageContext storageContext)
        { 
            //this.storageContext = storageContext as StorageContext;
            //this.dbSet = this.storageContext.Set<Item>();
        }

        public int Add(Event entity)
        {
            entity.CreationDate = DateTime.UtcNow;
            var entry = _db.Events.Add(entity);
            _db.SaveChanges();
            return entry.Entity.EventId;
        }

        public IEnumerable<Event> AllIncluding<TProp>(params Expression<Func<Event, TProp>>[] includeProperties)
        {
            IIncludableQueryable<Event, TProp> query = null;
            if (includeProperties.Length > 0)
            {
                query = _db.Events.Include(includeProperties[0]);
            }
            for (int queryIndex = 1; queryIndex < includeProperties.Length; ++queryIndex)
            {
                query = query.Include(includeProperties[queryIndex]);
            }
            return query == null ? All() : (IQueryable<Event>)query;
        }
    }
}
