using EmployeeManagementAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Department> Department { get; }
        IGenericRepository<Employee> Employee { get; }

        Task Save();
    }
}
