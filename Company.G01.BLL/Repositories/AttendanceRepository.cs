using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Repositories
{
	public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
	{
		public AttendanceRepository(AppDbContext context) : base(context)
		{
		}
	}
}
