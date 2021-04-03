using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Data
{
    public class Employee
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string IdentityNo { get; set; }
        public string EmployeeName { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
        public string Address { get; set; }

        [ForeignKey(nameof(Department))]
        public int DepartmentID { get; set; }
        public Department Department { get; set; }
        


    }
}
