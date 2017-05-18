using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SynWebCRM.Web.Data;

namespace SynWebCRM.Web.Repository
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

        public IQueryable<Event> All()
        {
            return _db.Events;
        }

        public IQueryable<Event> AllIncluding(params Expression<Func<Event, object>>[] includeProperties)
        {
            throw new NotImplementedException();
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
    }
}
