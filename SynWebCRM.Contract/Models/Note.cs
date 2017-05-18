using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SynWebCRM.Contract.Models
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
