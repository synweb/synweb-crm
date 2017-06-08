using System;
using Microsoft.AspNetCore.Mvc;
using SynWebCRM.Web.ApiControllers.Models;
using SynWebCRM.Web.Models;
using SynWebCRM.Data.EF;
using SynWebCRM.Contract.Models;
using SynWebCRM.Contract.Repositories;

namespace SynWebCRM.Web.ApiControllers
{

    [Route("api/[controller]")]
    public class CustomersApiController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly INoteRepository _noteRepository;

        public CustomersApiController(ICustomerRepository customerRepository, INoteRepository noteRepository)
        {
            _customerRepository = customerRepository;
            _noteRepository = noteRepository;
        }


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
                newNote.Customer = cust;
                _noteRepository.Add(newNote);
                return new ResultModel(true, newNote.NoteId);
            }
            catch (Exception e)
            {
                return new ResultModel(e);
            }
        }
    }
}