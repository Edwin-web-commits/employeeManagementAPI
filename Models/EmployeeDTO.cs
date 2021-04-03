using EmployeeManagementAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Models
{
    public class CreateEmployeeDTO
    {
       
        public string Email { get; set; }
        public string IdentityNo { get; set; }
        public string EmployeeName { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
        public string Address { get; set; }

      
        public int DepartmentID { get; set; }

        
    }
    public class EmployeeDTO: CreateEmployeeDTO
    {
        public int Id { get; set; }
        public DepartmentDTO Department { get; set; }
    }
    public class UpdateEmployeeDTO: CreateEmployeeDTO
    {
    }
}
