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
    public class DealStateRepository: IDealStateRepository
    {
        public DealStateRepository(CRMModel db)
        {
            _db = db;
        }

        private readonly CRMModel _db;

        public void SetStorageContext(IStorageContext storageContext)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DealState> All()
        {
            return _db.DealStates.OrderBy(x => x.Order).ToList();
        }

        public IEnumerable<DealState> AllIncluding<TProp>(params Expression<Func<DealState, TProp>>[] includeProperties)
        {
            IIncludableQueryable<DealState, TProp> query = null;
            if (includeProperties.Length > 0)
            {
                query = _db.DealStates.Include(includeProperties[0]);
            }
            for (int queryIndex = 1; queryIndex < includeProperties.Length; ++queryIndex)
            {
                query = query.Include(includeProperties[queryIndex]);
            }
            return query == null ? All() : (IQueryable<DealState>)query;
        }

        public DealState GetById(int id)
        {
            return _db.DealStates.SingleOrDefault(x => x.DealStateId == id);
        }

        public int Add(DealState entity)
        {
            var rec = _db.Add(entity);
            _db.SaveChanges();
            return rec.Entity.DealStateId;
        }


        public void Update(DealState entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(DealState entity)
        {
            _db.Remove(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var rec = _db.DealStates.Find(id);
            Delete(rec);
        }
    }
}
