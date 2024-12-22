using Microsoft.EntityFrameworkCore;

namespace CFirstApproach.Models
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Employee> Employees { get; set; } 
    }
}
