using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public UnitOfWork(AppDbContext context)
        {
            _employeeRepository = new EmployeeRepository(context);
            _departmentRepository = new DepartmentRepository(context);
            _context = context;

        }
        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        public IEmployeeRepository EmployeeRepository => _employeeRepository;

        //public AppDbContext Context { get; }
        public async Task<int> CompleteAsync()
        {
          return await _context.SaveChangesAsync();
        }
    }
}

