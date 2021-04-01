using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Data
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }

        public virtual IList<Employee> Employees { get; set; }

    }
}
