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
    class NoteRepository: INoteRepository
    {
        public NoteRepository(CRMModel db)
        {
            _db = db;
        }

        private readonly CRMModel _db;

        public void SetStorageContext(IStorageContext storageContext)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Note> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Note> AllIncluding<TProp>(params Expression<Func<Note, TProp>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Note GetById(int id)
        {
            return _db.Notes.SingleOrDefault(x => x.NoteId == id);
        }

        public IEnumerable<Note> GetByCustomer(int customerId)
        {
            return _db.Notes.Where(x => x.Customer.CustomerId == customerId).ToList();
        }

        public IEnumerable<Note> GetByDeal(int dealId)
        {
            return _db.Notes.Where(x => x.Deal.DealId == dealId).ToList();
        }

        public int Add(Note entity)
        {
            var rec = _db.Add(entity);
            _db.SaveChanges();
            return rec.Entity.NoteId;
        }

        public void Update(Note entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(Note entity)
        {
            _db.Remove(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var rec = _db.Notes.Find(id);
            Delete(rec);
        }
    }
}
