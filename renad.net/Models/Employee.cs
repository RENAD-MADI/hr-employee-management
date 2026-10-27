using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace renad.net.Models
{
    public class Employee
    {
        [Key]
        public int Employee_Id { get; set; }

        [Required, StringLength(25)]
        [Display(Name = "Name")]
        public string Employee_Name { get; set; } = string.Empty;

        [Required, StringLength(50), EmailAddress]
        [Display(Name = "Email")]
        public string Employee_Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Date)]
        [Display(Name = "Hire date")]
        public DateTime Employee_Hire_Data { get; set; } = DateTime.Today;

        [Required, Phone]
        [Display(Name = "Phone")]
        public string Employee_Phone { get; set; } = string.Empty;

        [Required, Range(0, 1_000_000)]
        [Column(TypeName = "float")]
        [Display(Name = "Salary")]
        public double Employee_Salary { get; set; }

        [Display(Name = "Active")]
        public bool Is_Active { get; set; } = true;

        [Required(ErrorMessage = "Please choose a department")]
        [Display(Name = "Department")]
        public int Department_Id { get; set; }

        [ForeignKey("Department_Id")]
        public Department? Department { get; set; }

        [Required(ErrorMessage = "Please choose a gender")]
        [Display(Name = "Gender")]
        public int Gender_id { get; set; }

        [ForeignKey("Gender_id")]
        public Gender? Gender { get; set; }
    }
}
