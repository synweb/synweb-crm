using Microsoft.AspNetCore.Mvc;
using SynWebCRM.Contract.Repositories;
using SynWebCRM.Data.EF;
using SynWebCRM.Web.Models;
using SynWebCRM.Web.Security;

namespace SynWebCRM.Web.ApiControllers
{
    [Route("api/[controller]")]
    public class NotesApiController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NotesApiController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpPost]
        [Route("/api/note/{id}/delete")]
        public ResultModel DeleteNote(int id)
        {
            var note = _noteRepository.GetById(id);
            if (!(User.Identity.Name == note.Creator || User.IsInRole(CRMRoles.Admin)))
                return ResultModel.Error;
            _noteRepository.Delete(note);
            return ResultModel.Success;
        }
    }
}