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

        [JsonPropertyName("cd_usuario")]
        public int UserId { get; set; }

        [JsonPropertyName("cd_login")]
        public string Login { get; set; } = string.Empty;

        [JsonPropertyName("nm_usuario")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("cd_situacao")]
        public string Situation { get; set; } = string.Empty;

        [JsonPropertyName("ds_email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("cd_idioma")]
        public string Language { get; set; } = string.Empty;

        [JsonPropertyName("nr_cpf")]
        public string Document { get; set; } = string.Empty;

        [JsonPropertyName("ds_cargo")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("ds_setor")]
        public string Department { get; set; } = string.Empty;

        [JsonPropertyName("fl_senha_login")]
        public bool PasswordStatus { get; set; }

        [JsonPropertyName("qt_erro_login")]
        public int QttyErrors { get; set; }

        [JsonPropertyName("cd_senha")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("Perfil")]
        public string Profile { get; set; } = string.Empty;

        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("cd_acesso_usuario")]
        public string AccessCode { get; set; } = string.Empty;

        [JsonPropertyName("expira_em")]
        Date? ExpirationDate { get; set; }

        [JsonPropertyName("key_adv")]
        public string AcadesKey { get; set; } = string.Empty;

    }
}
