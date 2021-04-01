using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<Department> _departments;
        private IGenericRepository<Employee> _employees;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IGenericRepository<Department> Department => _departments ??= new GenericRepository<Department>(_context);

        public IGenericRepository<Employee> Employee => _employees ??=new GenericRepository<Employee>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
