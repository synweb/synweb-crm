using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using SynWebCRM.Contract.Models;
using SynWebCRM.Contract.Repositories;

namespace SynWebCRM.Data.Mock
{
    public class CustomerRepository: ICustomerRepository
    {
        public CustomerRepository()
        {
            _identity = new IntAutoincrementor();
            _customers.Add(new Customer(){
                CustomerId = _identity.NextValue,
                VkId = "ololoid",
                Phone = "79998887777",
                CreationDate = new DateTime(1970,1,1),
                Email = "semenov@synweb.ru",
                Name = "Srg Semenoff",
                NeedsAttention = false,
                Description = "Eto ya",
                Source = CustomerSource.Friend
            });
        }

        public void SetStorageContext(IStorageContext storageContext)
        {
        }

        private List<Customer> _customers = new List<Customer>();
        private static IntAutoincrementor _identity;

        public IEnumerable<Customer> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> AllIncluding<TProp>(params Expression<Func<Customer, TProp>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Customer GetById(int id)
        {
            return _customers.SingleOrDefault(x => x.CustomerId == id);
        }

        public int Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> AllWithDeals()
        {
            throw new NotImplementedException();
        }
    }
}
