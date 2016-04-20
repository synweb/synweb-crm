using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SynWebCRM.ApiControllers.Models
{
    public class AddNoteData<T>
    {
        public T TargetId { get; set; }
        public string Text { get; set; }
    }
}