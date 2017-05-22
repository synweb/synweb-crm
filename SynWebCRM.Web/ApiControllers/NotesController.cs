using Microsoft.AspNetCore.Mvc;
using SynWebCRM.Data.EF;
using SynWebCRM.Web.Models;
using SynWebCRM.Web.Security;

namespace SynWebCRM.Web.ApiControllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private CRMModel _crmModel;

        public NotesController(CRMModel crmModel)
        {
            _crmModel = crmModel;
        }

        [HttpPost]
        [Route("/api/note/{id}/delete")]
        public ResultModel DeleteNote(int id)
        {
            var note = _crmModel.Notes.Find(id);
            if (!(User.Identity.Name == note.Creator
                  || User.IsInRole(CRMRoles.Admin)))
                return ResultModel.Error;
            _crmModel.Notes.Remove(note);
            _crmModel.SaveChanges();
            return ResultModel.Success;
        }
    }
}