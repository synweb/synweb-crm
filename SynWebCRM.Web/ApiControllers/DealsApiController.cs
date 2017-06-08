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
using SynWebCRM.Contract.Repositories;

namespace SynWebCRM.Web.ApiControllers
{
    [Route("api/[controller]")]
    public class DealsApiController : Controller
    {
        private readonly IDealRepository _dealRepository;
        private readonly INoteRepository _noteRepository;

        public DealsApiController(IDealRepository dealRepository, INoteRepository noteRepository)
        {
            _dealRepository = dealRepository;
            _noteRepository = noteRepository;
        }

        [HttpGet]
        [Route("/api/deals/get")]
        public ICollection<DealModel> GetDeals()
        {
            List<Deal> dataRes = _dealRepository.All().ToList();
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
                newNote.Deal = deal;
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