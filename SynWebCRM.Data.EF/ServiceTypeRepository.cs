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
    public class ServiceTypeRepository: IServiceTypeRepository
    {
        private readonly CRMModel _db;

        public ServiceTypeRepository(CRMModel db)
        {
            _db = db;
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServiceType> All()
        {
            return _db.ServiceTypes.ToList();
        }

        public IEnumerable<ServiceType> AllIncluding<TProp>(params Expression<Func<ServiceType, TProp>>[] includeProperties)
        {
            IIncludableQueryable<ServiceType, TProp> query = null;
            if (includeProperties.Length > 0)
            {
                query = _db.ServiceTypes.Include(includeProperties[0]);
            }
            for (int queryIndex = 1; queryIndex < includeProperties.Length; ++queryIndex)
            {
                query = query.Include(includeProperties[queryIndex]);
            }
            return query == null ? All() : (IQueryable<ServiceType>)query;
        }

        public ServiceType GetById(int id)
        {
            return _db.ServiceTypes.SingleOrDefault(x => x.ServiceTypeId == id);
        }

        public int Add(ServiceType entity)
        {
            var rec = _db.Add(entity);
            _db.SaveChanges();
            return rec.Entity.ServiceTypeId;
        }

        public void Update(ServiceType entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(ServiceType entity)
        {
            _db.Remove(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var rec = _db.ServiceTypes.Find(id);
            Delete(rec);
        }
    }
}
