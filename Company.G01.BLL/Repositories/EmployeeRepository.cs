using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Employee>> GetByNameAsync(string name)
        {
          return  await _Context.employee.Where(E => E.Name.ToLower().Contains(name.ToLower())).Include(E => E.WorkFor).ToListAsync();
            
        }
    }
}
