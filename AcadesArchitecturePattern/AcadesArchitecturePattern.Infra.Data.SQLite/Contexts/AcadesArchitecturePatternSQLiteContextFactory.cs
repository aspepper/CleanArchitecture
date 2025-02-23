using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AcadesArchitecturePattern.Infra.Data.SQLite.Contexts;

public class AcadesArchitecturePatternSQLiteContextFactory : IDesignTimeDbContextFactory<AcadesArchitecturePatternSQLiteContext>
{
    public AcadesArchitecturePatternSQLiteContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AcadesArchitecturePatternSQLiteContext>();

        // Configure a conexão com SQLite; por exemplo:
        optionsBuilder.UseSqlite("Data Source=AcadesArchitecturePatternDb.db");

        return new AcadesArchitecturePatternSQLiteContext(optionsBuilder.Options);
    }
}
