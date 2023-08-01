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
        public string Login { get; set; }

        [JsonPropertyName("nm_usuario")]
        public string Name { get; set; }

        [JsonPropertyName("cd_situacao")]
        public string Situation { get; set; }

        [JsonPropertyName("ds_email")]
        public string Email { get; set; }

        [JsonPropertyName("cd_idioma")]
        public string Language { get; set; }

        [JsonPropertyName("nr_cpf")]
        public string Document { get; set; }

        [JsonPropertyName("ds_cargo")]
        public string Role { get; set; }

        [JsonPropertyName("ds_setor")]
        public string Department { get; set; }

        [JsonPropertyName("fl_senha_login")]
        public bool PasswordStatus { get; set; }

        [JsonPropertyName("qt_erro_login")]
        public int QttyErrors { get; set; }

        [JsonPropertyName("cd_senha")]
        public string Password { get; set; }

        [JsonPropertyName("Perfil")]
        public string Profile { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("cd_acesso_usuario")]
        public string AccessCode { get; set; }

        [JsonPropertyName("expira_em")]
        Date ExpirationDate { get; set; }

        [JsonPropertyName("key_adv")]
        public string AcadesKey { get; set; }

    }
}
