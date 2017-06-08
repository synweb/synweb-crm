using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SynWebCRM.Contract.Models;
using SynWebCRM.Contract.Repositories;

namespace SynWebCRM.Data.EF
{
    public class EstimateItemRepository: IEstimateItemRepository
    {
        private readonly CRMModel _db;

        public EstimateItemRepository(CRMModel db)
        {
            _db = db;
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EstimateItem> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EstimateItem> AllIncluding<TProp>(params Expression<Func<EstimateItem, TProp>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EstimateItem> GetByEstimateId(int id)
        {
            return _db.EstimateItems.Where(x => x.EstimateId == id)
                .OrderBy(x => x.SortOrder).ThenBy(x => x.CreationDate)
                .ToList();
        }

        public EstimateItem GetById(int id)
        {
            return _db.EstimateItems.Find(id);
        }

        public int Add(EstimateItem entity)
        {
            var rec = _db.Add(entity);
            _db.SaveChanges();
            return rec.Entity.ItemId;
        }

        public void Update(EstimateItem entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(EstimateItem entity)
        {
            _db.EstimateItems.Remove(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Delete(GetById(id));
        }
    }
}
