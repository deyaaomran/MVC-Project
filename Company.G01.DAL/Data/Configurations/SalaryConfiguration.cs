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
	public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
	{
		public void Configure(EntityTypeBuilder<Salary> builder)
		{
			builder.HasOne(e => e.Employee)
				.WithMany()
				.HasForeignKey(e => e.EmployeeId)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
