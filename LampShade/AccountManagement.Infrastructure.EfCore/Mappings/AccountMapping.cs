﻿using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mappings
{
    public class AccountMapping:IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName).HasMaxLength(200);
            builder.Property(x => x.UserName).HasMaxLength(200);
            builder.Property(x => x.Password).HasMaxLength(200);
            builder.Property(x => x.Mobile).HasMaxLength(13);
            builder.Property(x => x.ProfilePhoto).HasMaxLength(400);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.RoleId);


        }
    }
}
