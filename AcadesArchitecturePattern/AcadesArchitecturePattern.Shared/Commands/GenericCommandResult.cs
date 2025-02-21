namespace AcadesArchitecturePattern.Shared.Commands
{
    public class GenericCommandResult(bool success, string message, object data) : ICommandResult
    {
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
        public Object Data { get; set; } = data;
    }
}
