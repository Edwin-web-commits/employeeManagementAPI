using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Models
{
    public class CreateDepartmentDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Department Name is too long")]
        public string DapartmentName { get; set; }

    }
    public class DepartmentDTO : CreateDepartmentDTO
    {
        public int Id { get; set; }
        public IList<EmployeeDTO> Employees { get; set; }
    }
    public class UpdateDepartmentDTO : CreateDepartmentDTO
    {
        public IList<CreateEmployeeDTO> Employees { get; set; }
    }
}
