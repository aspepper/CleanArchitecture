using AcadesArchitecturePattern.Shared.Entities;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AcadesArchitecturePattern.Domain.Entities
{
    /// <summary>
    /// Entidade para consumo de API, não é necessário configurá-la no Infra.Data
    /// </summary>
    public class AuthenticateResponse : Base
    {

        [JsonPropertyName("UserId")]
        public int UserId { get; set; }

        [JsonPropertyName("LoginName")]
        public string Login { get; set; } = string.Empty;

        [JsonPropertyName("UserName")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("Situation")]
        public string Situation { get; set; } = string.Empty;

        [JsonPropertyName("Email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("Language")]
        public string Language { get; set; } = string.Empty;

        [JsonPropertyName("DocumentID")]
        public string Document { get; set; } = string.Empty;

        [JsonPropertyName("Role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("Department")]
        public string Department { get; set; } = string.Empty;

        [JsonPropertyName("PasswordStatus")]
        public bool PasswordStatus { get; set; }

        [JsonPropertyName("LoginErros")]
        public int QttyErrors { get; set; }

        [JsonPropertyName("Password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("Profile")]
        public string Profile { get; set; } = string.Empty;

        [JsonPropertyName("Token")]
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("AccessCode")]
        public string AccessCode { get; set; } = string.Empty;

        [JsonPropertyName("ExpirationDate")]
        Date? ExpirationDate { get; set; }

        [JsonPropertyName("AcadesKey")]
        public string AcadesKey { get; set; } = string.Empty;

    }
}
