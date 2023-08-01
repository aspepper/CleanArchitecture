using AcadesArchitecturePattern.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcadesArchitecturePattern.Infra.Data.Mappings
{
    public class TaskMapping : IEntityTypeConfiguration<Domain.Entities.ToDoTask>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ToDoTask> builder)
        {
            // Adding Table Name
            builder.ToTable("Tasks");

            // Adding Id
            builder.Property(x => x.Id);

            // Adding ToDoList's FK
            builder.HasOne<ToDoList>(x => x.Lists)
                    .WithMany(x => x.Tasks)
                    .HasForeignKey(x => x.IdList).OnDelete(DeleteBehavior.Restrict);

            // Adding Name
            builder.Property(x => x.Name).HasColumnType("VARCHAR(50)");
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Name).IsRequired();

            // Adding Description
            builder.Property(x => x.Description).HasColumnType("VARCHAR(200)");
            builder.Property(x => x.Description).HasMaxLength(200);
            builder.Property(x => x.Description).IsRequired();

            // Adding Reminder
            builder.Property(x => x.InsertDate).HasColumnType("DATETIME");
            builder.Property(x => x.InsertDate).HasDefaultValueSql("GETDATE()");

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
