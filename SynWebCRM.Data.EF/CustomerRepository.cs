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
    public class CustomerRepository: ICustomerRepository
    {
        private CRMModel _db;

        public CustomerRepository(CRMModel db)
        {
            _db = db;
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> All()
        {
            return _db.Customers.ToList();
        }

        public IEnumerable<Customer> AllIncluding<TProp>(params Expression<Func<Customer, TProp>>[] includeProperties)
        {
            IIncludableQueryable<Customer, TProp> query = null;
            if (includeProperties.Length > 0)
            {
                query = _db.Customers.Include(includeProperties[0]);
            }
            for (int queryIndex = 1; queryIndex < includeProperties.Length; ++queryIndex)
            {
                query = query.Include(includeProperties[queryIndex]);
            }
            return query == null ? All() : (IQueryable<Customer>)query;
        }

        public Customer GetById(int id)
        {
            return _db.Customers
                .Include(x => x.Deals)
                .Include(x => x.Notes)
                .Include(x => x.Websites)
                .SingleOrDefault(x => x.CustomerId == id);
        }

        public int Add(Customer entity)
        {
            var rec = _db.Add(entity);
            _db.SaveChanges();
            return rec.Entity.CustomerId;
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Customer entity)
        {
            _db.Remove(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var rec = _db.Customers.Find(id);
            _db.Remove(rec);
            _db.SaveChanges();
        }

        public IEnumerable<Customer> AllWithDeals()
        {
            return _db.Customers.Include(x => x.Deals).ThenInclude(x => x.DealState).ToList();
        }
    }
}
