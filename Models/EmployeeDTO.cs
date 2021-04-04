using EmployeeManagementAPI.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Models
{
    public class CreateEmployeeDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string IdentityNo { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Employee Name is too long")]
        public string EmployeeName { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Position Name is too long")]
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
    public class LoginUserDTO: CreateEmployeeDTO
    {
    }
}
