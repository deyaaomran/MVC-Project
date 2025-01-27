using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
		public IAttendanceRepository AttendanceRepository   { get; }
		public IEmployeeProjectRepository EmployeeProjectRepository { get; }
		public IPositionRepository PositionRepository { get; }
		public IProjectRepository ProjectRepository { get; }
		public ISalaryRepository SalaryRepository { get; }


		public Task<int> CompleteAsync();


    }
}
