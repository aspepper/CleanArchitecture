namespace AcadesArchitecturePattern.Shared;

public static class DatabasePathHelper
{
    public static string GetDatabasePath()
    {
        // Obtém a pasta local de dados do usuário (pode ser diferente entre sistemas)
        var baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        // Define uma subpasta para a sua aplicação
        var appFolder = Path.Combine(baseFolder, "AcadesArchitecturePattern");

        // Garante que a pasta exista
        Directory.CreateDirectory(appFolder);

        // Define o caminho completo do arquivo .db
        var dbPath = Path.Combine(appFolder, "AcadesArchitecturePatternDb.db");

        return dbPath;
    }
}
