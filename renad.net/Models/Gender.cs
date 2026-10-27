using renad.net.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace renad.net.Models
{
    public class Gender
    {
        [Key]
        public int Gender_Id { get; set; }

        [Required]
        public string Gender_Name { get; set; } = string.Empty;

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}