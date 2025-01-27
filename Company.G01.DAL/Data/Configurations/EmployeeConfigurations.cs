using Company.G01.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.DAL.Data.Configurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            
            builder.HasOne(e => e.Position)
                   .WithMany()
                   .HasForeignKey(e => e.PositionId)
                   .OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(e => e.SalaryFor)
					   .WithMany()
					   .HasForeignKey(e => e.SaralryForId)
					   .OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(e => e.WorkFor)
	               .WithMany()
	               .HasForeignKey(e => e.WorkForId)
	               .OnDelete(DeleteBehavior.SetNull);

		}
        
    }
}
