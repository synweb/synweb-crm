using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SynWebCRM.Web.ApiControllers.Models;
using SynWebCRM.Web.Models;
using SynWebCRM.Data.EF;
using SynWebCRM.Contract.Models;

namespace SynWebCRM.Web.ApiControllers
{
    [Route("api/[controller]")]
    public class DealsApiController : Controller
    {
        private CRMModel _crmModel;

        public DealsApiController(CRMModel crmModel)
        {
            _crmModel = crmModel;
        }

        [HttpGet]
        [Route("/api/deals/get")]
        public ICollection<DealModel> GetDeals()
        {
            List<Deal> dataRes = _crmModel.Deals
                .Include(x => x.Customer)
                .Include(x => x.DealState)
                .OrderByDescending(x => x.NeedsAttention)
                .ThenByDescending(x => x.CreationDate).ToList();
            var res = Mapper.Map<ICollection<DealModel>>(dataRes);
            return res;
        }

        [HttpPost]
        [Route("/api/deals/notes/add")]
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
                var deal = new Deal() { DealId = note.TargetId };
                _crmModel.Deals.Attach(deal);
                newNote.Deal = deal;
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