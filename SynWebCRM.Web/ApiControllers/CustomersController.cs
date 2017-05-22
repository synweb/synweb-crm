using System;
using Microsoft.AspNetCore.Mvc;
using SynWebCRM.Web.ApiControllers.Models;
using SynWebCRM.Web.Models;
using SynWebCRM.Data.EF;
using SynWebCRM.Contract.Models;

namespace SynWebCRM.Web.ApiControllers
{

    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        public CustomersController(CRMModel crmModel)
        {
            _crmModel = crmModel;
        }

        private readonly CRMModel _crmModel;

        [HttpPost]
        [Route("/api/customers/notes/add")]
        public ResultModel AddNote(AddNoteData<int> note)
        {
            try
            {
                var newNote = new Note
                {
                    Text = note.Text,
                    CreationDate = DateTime.Now,
                    Creator = User.Identity.Name,

                };
                var cust = new Customer() { CustomerId = note.TargetId };
                _crmModel.Customers.Attach(cust);
                newNote.Customer = cust;
                _crmModel.Notes.Add(newNote);
                _crmModel.SaveChanges();
                return new ResultModel(true, newNote.NoteId);
            }
            catch (Exception e)
            {
                return new ResultModel(e);
            }
        }
    }
}