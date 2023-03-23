using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mappings
{
    public class RoleMapping:IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100);

            builder.OwnsMany(x => x.Permissions, navigationBuilder =>
            {
                navigationBuilder.HasKey(x => x.Id);
                navigationBuilder.ToTable("RolePermissions");
                navigationBuilder.Ignore(x => x.Name);
                navigationBuilder.WithOwner(x => x.Role);
            });
            builder.HasMany(x => x.Accounts)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId);

         
        }
    }
}
