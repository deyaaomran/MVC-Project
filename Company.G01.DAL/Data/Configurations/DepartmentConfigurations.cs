using Company.G01.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.DAL.Data.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(P=>P.id).UseIdentityColumn(10,10);
            builder.Property(U => U.name).IsRequired();
            builder.Property(U => U.Code).IsRequired();
            
        }
    }
}
