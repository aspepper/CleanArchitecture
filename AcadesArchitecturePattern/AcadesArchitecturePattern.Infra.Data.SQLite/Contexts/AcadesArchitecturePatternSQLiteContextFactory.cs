using AcadesArchitecturePattern.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AcadesArchitecturePattern.Infra.Data.SQLite.Contexts;

public class AcadesArchitecturePatternSQLiteContextFactory : IDesignTimeDbContextFactory<AcadesArchitecturePatternSQLiteContext>
{
    public AcadesArchitecturePatternSQLiteContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AcadesArchitecturePatternSQLiteContext>();

        var dbPath = DatabasePathHelper.GetDatabasePath();
        var connectionString = $"Data Source={dbPath}";
        
         // Configure a conexão com SQLite; por exemplo:
        optionsBuilder.UseSqlite(connectionString);

        return new AcadesArchitecturePatternSQLiteContext(optionsBuilder.Options);
    }
}
