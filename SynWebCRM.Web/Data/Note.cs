using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynWebCRM.Web.Data
{
    [Table("Note")]
    public class Note
    {
        public Note()
        {
        }

        public int NoteId { get; set; }

        public DateTime CreationDate { get; set; }

        public string Text { get; set; }
        public string Creator { get; set; }

        public Customer Customer { get; set; }
        public Deal Deal { get; set; }
    }
}
