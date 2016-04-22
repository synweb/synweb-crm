using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SynWebCRM.Data;
using SynWebCRM.Models;
using SynWebCRM.Security;

namespace SynWebCRM.ApiControllers
{
    public class NotesApiController:ApiController
    {
        Model db = new Model();

        [HttpPost]
        public ResultModel DeleteNote(int id)
        {
            var note = db.Notes.Find(id);
            if (!(User.Identity.Name == note.Creator
                  || User.IsInRole(CRMRoles.Admin)))
                return ResultModel.Error;
            db.Notes.Remove(note);
            db.SaveChanges();
            return ResultModel.Success;
        }
    }
}