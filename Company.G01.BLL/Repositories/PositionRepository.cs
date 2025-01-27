using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Repositories
{
	public class PositionRepository : GenericRepository<Position>, IPositionRepository
	{
		public PositionRepository(AppDbContext context) : base(context)
		{
		}
	}
}
