using AcadesArchitecturePattern.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AcadesArchitecturePattern.Infra.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Adding Table Name
            builder.ToTable("Users");

            // Adding Id
            builder.Property(x => x.Id);

            // Adding UserName
            builder.Property(x => x.UserName).HasColumnType("VARCHAR(50)");
            builder.Property(x => x.UserName).HasMaxLength(50);
            builder.Property(x => x.UserName).IsRequired();
            builder.HasIndex(x => x.UserName).IsUnique();

            // Adding Email
            builder.Property(x => x.Email).HasColumnType("VARCHAR(60)");
            builder.Property(x => x.Email).HasMaxLength(60);
            builder.Property(x => x.Email).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();

            // Adding Password
            builder.Property(x => x.Password).HasColumnType("VARCHAR(60)");
            builder.Property(x => x.Password).HasMaxLength(60);
            builder.Property(x => x.Password).IsRequired();

            // Adding InsertDate
            builder.Property(x => x.InsertDate).HasColumnType("DATETIME");
            builder.Property(x => x.InsertDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.InsertDate).IsRequired();

            // Adding InsertedBy
            builder.Property(x => x.InsertedBy).HasColumnType("VARCHAR(50)");
            builder.Property(x => x.InsertedBy).HasMaxLength(50);
            builder.Property(x => x.InsertDate).IsRequired();

            // Adding ModifyDate
            builder.Property(x => x.ModifyDate).HasColumnType("DATETIME");
            builder.Property(x => x.ModifyDate).HasDefaultValueSql("GETDATE()");

            // Adding ModifiedBy
            builder.Property(x => x.ModifiedBy).HasColumnType("VARCHAR(50)");
            builder.Property(x => x.ModifiedBy).HasMaxLength(50);
        }
    }
}
