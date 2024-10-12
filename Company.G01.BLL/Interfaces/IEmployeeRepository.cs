using Company.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository <Employee>
    {
        Task<IEnumerable<Employee>> GetByNameAsync(string name);
         /*
                Employee Get(string Name);
         */

        //IEnumerable<Employee> Getall();
        //Employee Get(int id);
        //int Update(Employee entity);
        //int Delete(Employee entity);
        //int Add(Employee entity);
    }
}
