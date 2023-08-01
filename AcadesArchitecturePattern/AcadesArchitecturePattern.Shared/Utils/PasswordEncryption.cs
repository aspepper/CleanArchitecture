namespace AcadesArchitecturePattern.Shared.Utils
{
    public static class PasswordEncryption
    {
        public static string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool ValidateHashes(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
