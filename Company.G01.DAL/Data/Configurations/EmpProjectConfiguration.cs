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
	public class EmpProjectConfiguration : IEntityTypeConfiguration<EmployeeProject>
	{
		public void Configure(EntityTypeBuilder<EmployeeProject> builder)
		{
			builder.HasOne(e => e.Employee)
				.WithMany()
				.HasForeignKey(e => e.EmployeeId)
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(e => e.Project)
				.WithMany()
				.HasForeignKey(e => e.ProjectId)
				.OnDelete(DeleteBehavior.SetNull);

		}
	}
}
