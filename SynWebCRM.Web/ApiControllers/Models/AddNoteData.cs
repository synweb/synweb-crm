namespace SynWebCRM.Web.ApiControllers.Models
{
    public class AddNoteData<T>
    {
        public T TargetId { get; set; }
        public string Text { get; set; }
    }
}