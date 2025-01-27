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
        private readonly IEmployeeProjectRepository _employeeProjectRepository;
        private readonly ISalaryRepository _salaryRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IPositionRepository _positionRepository;
        public UnitOfWork(AppDbContext context)
        {
            _employeeRepository = new EmployeeRepository(context);
            _departmentRepository = new DepartmentRepository(context);
			_employeeProjectRepository = new EmployeeProjectRepository(context);
			_salaryRepository = new SalaryRepository(context);
            _attendanceRepository = new AttendanceRepository(context);
            _projectRepository = new ProjectRepository(context);
            _positionRepository = new PositionRepository(context);



			_context = context;

        }
        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        public IEmployeeRepository EmployeeRepository => _employeeRepository;

		public IAttendanceRepository AttendanceRepository => _attendanceRepository;

		public IEmployeeProjectRepository EmployeeProjectRepository => _employeeProjectRepository;

		public IPositionRepository PositionRepository => _positionRepository;

		public IProjectRepository ProjectRepository => _projectRepository;

		public ISalaryRepository SalaryRepository => _salaryRepository;

		//public AppDbContext Context { get; }
		public async Task<int> CompleteAsync()
        {
          return await _context.SaveChangesAsync();
        }
    }
}

