namespace SynWebCRM.Web.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Employee")]
    public partial class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Function { get; set; }
        [Display(Name = "Создатель")]
        public string Creator { get; set; }
    }
}
