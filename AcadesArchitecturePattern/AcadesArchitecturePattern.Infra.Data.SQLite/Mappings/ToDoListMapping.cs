using AcadesArchitecturePattern.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcadesArchitecturePattern.Infra.Data.SQLite.Mappings
{
    public class ToDoListMapping : IEntityTypeConfiguration<ToDoList>
    {
        public void Configure(EntityTypeBuilder<ToDoList> builder)
        {
            // Adding Table Name
            builder.ToTable("ToDoLists");

            // Adding Id
            builder.Property(x => x.Id);

            // Adding User's FK
            builder.HasOne<User>(x => x.Users)
                    .WithMany(x => x.ToDoLists)
                    .HasForeignKey(x => x.IdUser).OnDelete(DeleteBehavior.Restrict);

            // Adding Title
            builder.Property(x => x.Title).HasColumnType("VARCHAR(50)");
            builder.Property(x => x.Title).HasMaxLength(50);
            builder.Property(x => x.Title).IsRequired();

            // Adding InsertDate
            builder.Property(x => x.InsertDate).HasColumnType("DATETIME");
            builder.Property(x => x.InsertDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.InsertDate).IsRequired();

            // Adding InsertedBy
            builder.Property(x => x.InsertedBy).HasColumnType("VARCHAR(50)");
            builder.Property(x => x.InsertedBy).HasMaxLength(50);
            builder.Property(x => x.InsertDate).IsRequired();

            // Adding ModifyDate
            builder.Property(x => x.ModifyDate).HasColumnType("DATETIME");
            builder.Property(x => x.ModifyDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Adding ModifiedBy
            builder.Property(x => x.ModifiedBy).HasColumnType("VARCHAR(50)");
            builder.Property(x => x.ModifiedBy).HasMaxLength(50);
        }
    }
}
