using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.DAL.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server =.; Database = CompanyMVCG01; Trusted_Connection = True; TrustServerCertifcate = True");
        //}

        public DbSet<Department> department { get; set; }
        public DbSet<Employee> employee { get; set; }
        public DbSet<EmployeeProject> employeeProject { get; set; }
        public DbSet<Position> position { get; set; }
        public DbSet<Project> project { get; set; }    
        public DbSet<Attendance> attendance { get; set; }
        public DbSet<Salary> salary { get; set; }

        
    }
}
