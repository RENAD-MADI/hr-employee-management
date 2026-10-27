using System.ComponentModel.DataAnnotations;

namespace renad.net.Models
{
    public class Department
    {
        [Key]
        public int Department_Id { get; set; }

        [Required(ErrorMessage = "this field is required")]
        [StringLength(50, ErrorMessage = "please enter valied department name")]
        public string Department_Name { get; set; } = string.Empty;
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}