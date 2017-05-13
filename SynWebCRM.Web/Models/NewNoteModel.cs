namespace SynWebCRM.Web.Models
{
    public class NewNoteModel
    {
        public NewNoteModel(string addUrl, object targetId)
        {
            AddUrl = addUrl;
            TargetId = targetId;
        }

        public string AddUrl { get; private set; }
        public object TargetId { get; private set; }
    }
}