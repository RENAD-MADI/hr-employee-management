using Microsoft.EntityFrameworkCore;
using renad.net.Models;

namespace renad.net.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Gender> Genders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasIndex(e => e.Employee_Email).IsUnique();

            // Lookup values so the Create form has something to choose from.
            modelBuilder.Entity<Department>().HasData(
                new Department { Department_Id = 1, Department_Name = "Human Resources" },
                new Department { Department_Id = 2, Department_Name = "Finance" },
                new Department { Department_Id = 3, Department_Name = "IT" },
                new Department { Department_Id = 4, Department_Name = "Sales" });

            modelBuilder.Entity<Gender>().HasData(
                new Gender { Gender_Id = 1, Gender_Name = "Male" },
                new Gender { Gender_Id = 2, Gender_Name = "Female" });
        }
    }
}
