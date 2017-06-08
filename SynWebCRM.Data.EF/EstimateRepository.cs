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
    public class EstimateRepository: IEstimateRepository
    {
        private readonly CRMModel _db;

        public EstimateRepository(CRMModel db)
        {
            _db = db;
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Estimate> All()
        {
            return _db.Estimates
                .Include(x => x.Deal).ThenInclude(x => x.Customer).ToList();
        }

        public IEnumerable<Estimate> AllIncluding<TProp>(params Expression<Func<Estimate, TProp>>[] includeProperties)
        {

            IIncludableQueryable<Estimate, TProp> query = null;
            if (includeProperties.Length > 0)
            {
                query = _db.Estimates.Include(includeProperties[0]);
            }
            for (int queryIndex = 1; queryIndex < includeProperties.Length; ++queryIndex)
            {
                query = query.Include(includeProperties[queryIndex]);
            }
            return query == null ? All() : (IQueryable<Estimate>)query;
        }

        public Estimate GetById(int id)
        {
            return _db.Estimates.Find(id);
        }

        public int Add(Estimate entity)
        {
            var rec = _db.Add(entity);
            _db.SaveChanges();
            return rec.Entity.EstimateId;
        }

        public void Update(Estimate entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(Estimate entity)
        {
            _db.Remove(entity);
            var items = _db.EstimateItems.Where(x => x.EstimateId == entity.EstimateId);
            foreach (var estimateItem in items)
            {
                _db.Remove(estimateItem);
            }
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Delete(_db.Estimates.Single(x => x.EstimateId == id));
        }
    }
}
