using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SynWebCRM.Contract.Models;
using SynWebCRM.Contract.Repositories;

namespace SynWebCRM.Data.EF
{
    public class WebsiteRepository: IWebsiteRepository
    {
        private readonly CRMModel _db;

        public WebsiteRepository(CRMModel db)
        {
            _db = db;
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Website> All()
        {
            return _db.Websites.Include(x => x.Customer).ToList();
        }

        public IEnumerable<Website> AllIncluding<TProp>(params Expression<Func<Website, TProp>>[] includeProperties)
        {
            IIncludableQueryable<Website, TProp> query = null;
            if (includeProperties.Length > 0)
            {
                query = _db.Websites.Include(includeProperties[0]);
            }
            for (int queryIndex = 1; queryIndex < includeProperties.Length; ++queryIndex)
            {
                query = query.Include(includeProperties[queryIndex]);
            }
            return query == null ? All() : (IQueryable<Website>)query;
        }

        public Website GetById(int id)
        {
            return _db.Websites.Include(x => x.Customer).SingleOrDefault(x => x.WebsiteId == id);
        }

        public int Add(Website entity)
        {
            entity.CreationDate = DateTime.Now;
            var rec = _db.Add(entity);
            _db.SaveChanges();
            return rec.Entity.WebsiteId;
        }

        public void Update(Website entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(Website entity)
        {
            _db.Remove(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var rec = _db.Websites.Find(id);
            Delete(rec);
        }

        public ICollection<Website> GetEndingByDomain(DateTime start, DateTime end)
        {

            return _db.Websites.Where(x => x.IsActive
                                           && x.DomainEndingDate.HasValue
                                           && x.DomainEndingDate.Value >= start
                                           && x.DomainEndingDate.Value <= end)
                .ToList();
        }

        public ICollection<Website> GetEndingByHosting(DateTime start, DateTime end)
        {
            return _db.Websites.Where(x => x.IsActive
                                           && x.HostingEndingDate.HasValue
                                           && x.HostingEndingDate.Value >= start
                                           && x.HostingEndingDate.Value <= end)
                .ToList();
        }
    }
}
