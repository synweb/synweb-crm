using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SynWebCRM.ApiControllers.Models;
using SynWebCRM.Data;
using SynWebCRM.Models;

namespace SynWebCRM.ApiControllers
{
    public class CustomersApiController: ApiController
    {
        Model db = new Model();

        public ResultModel AddNote(AddNoteData<int> note)
        {
            try
            {
                var newNote = new Note
                {
                    Text = note.Text,
                    CreationDate = DateTime.Now,
                    Creator = User.Identity.Name
                };
                var cust = new Customer() {CustomerId = note.TargetId};
                db.Customers.Attach(cust);
                newNote.Customers.Add(cust);
                db.Notes.Add(newNote);
                db.SaveChanges();
                return new ResultModel(true, newNote.NoteId);
            }
            catch (Exception e)
            {
                return new ResultModel(e);
            }
        }
    }
}