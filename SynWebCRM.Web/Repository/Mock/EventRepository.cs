using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SynWebCRM.Web.Data;

namespace SynWebCRM.Web.Repository.Mock
{
    public class EventRepository: IEventRepository
    {
        public EventRepository()
        {
            _events.Add(new Event()
            {
                EventId = 1,
                Description = "Vodka Descr",
                Name = "Vodka",
                StartDate = new DateTime(2008, 1, 22, 15, 00, 00),
                EndDate = new DateTime(2008, 1, 22, 16, 00, 00),
                CreationDate = new DateTime(1600, 10, 05)
            });
            _events.Add(new Event()
            {
                EventId = 2,
                Description = "Vodka Descr 2",
                Name = "Vodka 2",
                StartDate = new DateTime(2008, 2, 22, 15, 00, 00),
                EndDate = new DateTime(2008, 2, 22, 16, 00, 00),
                CreationDate = new DateTime(1600, 10, 05)
            });

            _identity = new IntAutoincrementor(_events.Max(x => x.EventId));
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
            // nothing
        }

        private static List<Event> _events = new List<Event>();
        private static IntAutoincrementor _identity;

        public IQueryable<Event> All()
        {
            return _events.AsQueryable();
        }

        public IQueryable<Event> AllIncluding(params Expression<Func<Event, object>>[] includeProperties)
        {
            return _events.AsQueryable();
        }

        public Event GetById(int id)
        {
            return _events.Find(x => x.EventId == id);
        }

        public int Add(Event entity)
        {
            entity.EventId = _identity.NextValue;
            _events.Add(entity);
            return entity.EventId;
        }

        public void Update(Event entity)
        {
            var ent = GetById(entity.EventId);
            ent.Description = entity.Description;
            ent.EndDate = entity.EndDate;
            ent.Name = entity.Name;
            ent.StartDate = entity.StartDate;
        }

        public void Delete(Event entity)
        {
            _events.Remove(entity);
        }

        public void Delete(int id)
        {
            _events.RemoveAll(x => x.EventId == id);
        }
    }
}
