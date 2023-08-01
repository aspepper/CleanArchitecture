using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Infra.Data.Mappings;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace AcadesArchitecturePattern.Infra.Data.Contexts
{
    public class AcadesArchitecturePatternSqlServerContext : DbContext
    {
        public AcadesArchitecturePatternSqlServerContext(DbContextOptions<AcadesArchitecturePatternSqlServerContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<ToDoTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();

            // Entities Mappings
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new ToDoListMapping());
            modelBuilder.ApplyConfiguration(new TaskMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
