using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Repositories
{
    public class GenericRepository <T> : IGenericRepository<T> where T : BaseEntity
    {
        private protected readonly AppDbContext _Context;
        public GenericRepository(AppDbContext context)
        {
            _Context = context;
        }

        public async Task AddAsync(T entity)
        {
           await _Context.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _Context.Update(entity);
        }

        public void Delete(T entity)
        {
            _Context.Remove(entity);
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _Context.employee.Include(E => E.WorkFor).Include(E => E.Position).Include(E => E.SalaryFor).ToListAsync();
            }
            else if (typeof(T) == typeof(EmployeeProject))
            {
                return (IEnumerable<T>)await _Context.employeeProject.Include(E => E.Project).Include(E => E.Employee).ToListAsync();
            }
            else if(typeof(T) == typeof(Salary))
            {
				return (IEnumerable<T>)await _Context.salary.Include(E => E.Employee).ToListAsync();
			}
            else if (typeof(T) == typeof(Attendance))
            {
				return (IEnumerable<T>)await _Context.attendance.Include(E => E.Employee).ToListAsync();
			}

				return await _Context.Set<T>().ToListAsync();
        }

    }
}

