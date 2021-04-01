using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

           // builder.ApplyConfiguration(new RoleConfiguration());

            builder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    DepartmentName = "Human Resource"
                    
                },
                 new Department
                 {
                     Id = 2,
                     DepartmentName = "Sales"
                     
                 },
                  new Department
                  {
                      Id = 3,
                      DepartmentName = "Customs"
                      
                  }
                );
            builder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    IdentityNo="9509294764082",
                    Email="edwinmoropane02@gmail.com",
                    EmployeeName = "Moropane Motlokwa",
                    Address = "23 Nelson Mandel strt",
                    Position="Supervisor",
                    DepartmentID = 3,
                    Salary=50000.00
                    

                },
                 new Employee
                 {
                     Id = 2,
                     IdentityNo = "9909294764082",
                     Email = "edwinmoropane03@gmail.com",
                     EmployeeName = "Edwin Motlokwa",
                     Address = "29 Nelson Mandel strt",
                     Position = "Manager",
                     DepartmentID = 2,
                     Salary = 70000.00
                 },
                  new Employee
                  {
                      Id = 3,
                      IdentityNo = "9709294764082",
                      Email = "sharonmoropane04@gmail.com",
                      EmployeeName = "Sharon Motlokwa",
                      Address = "27 Nelson Mandel strt",
                      Position = "Supervisor",
                      DepartmentID = 1,
                      Salary = 50000.00
                  }
                );

        }
    }
}
