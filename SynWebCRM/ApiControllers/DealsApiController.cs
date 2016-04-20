using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using SynWebCRM.ApiControllers.Models;
using SynWebCRM.Data;
using SynWebCRM.Models;

namespace SynWebCRM.ApiControllers
{
    public class DealsApiController: ApiController
    {
        private Model db = new Model();

        [HttpGet]
        public ICollection<DealModel> GetDeals()
        {
            List<Deal> dataRes = db.Deals.OrderByDescending(x => x.NeedsAttention).ThenByDescending(x => x.CreationDate).ToList();
            var res = Mapper.Map<ICollection<DealModel>>(dataRes);
            return res;
        }


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
                db.Deals.Attach(deal);
                newNote.Deals.Add(deal);
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