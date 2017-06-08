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
    public class DealRepository: IDealRepository
    {
        private CRMModel _db;

        public DealRepository(CRMModel db)
        {
            _db = db;
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
            //
        }

        public IEnumerable<Deal> All()
        {
            return _db.Deals
                .Include(x => x.Customer)
                .Include(x => x.DealState)
                .Include(x => x.ServiceType)
                .OrderByDescending(x => x.NeedsAttention)
                .ThenByDescending(x => x.CreationDate)
                .ToList();
        }

        public IEnumerable<Deal> AllIncluding<TProp>(params Expression<Func<Deal, TProp>>[] includeProperties)
        {
            IIncludableQueryable<Deal, TProp> query = null;
            if (includeProperties.Length > 0)
            {
                query = _db.Deals.Include(includeProperties[0]);
            }
            for (int queryIndex = 1; queryIndex < includeProperties.Length; ++queryIndex)
            {
                query = query.Include(includeProperties[queryIndex]);
            }
            return query == null ? All() : (IQueryable<Deal>)query;
        }

        public Deal GetById(int id)
        {
            return _db.Deals
                .Include(x => x.Customer)
                .Include(x => x.Estimates)
                .Include(x => x.Notes)
                .SingleOrDefault(x => x.DealId == id);
        }

        public int Add(Deal entity)
        {
            var rec = _db.Add(entity);
            _db.SaveChanges();
            return rec.Entity.DealId;
        }

        public void Update(Deal entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(Deal entity)
        {
            _db.Remove(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var rec = _db.Deals.Find(id);
            Delete(rec);
        }

        public IEnumerable<Deal> GetLatestCompletedDeals(DateTime startDate)
        {
            return _db.Deals.Include(x => x.DealState).Include(x => x.ServiceType)
                .Where(x => x.DealState.IsCompleted && x.Profit.HasValue && x.CreationDate > startDate)
                .ToList();
        }
    }
}
