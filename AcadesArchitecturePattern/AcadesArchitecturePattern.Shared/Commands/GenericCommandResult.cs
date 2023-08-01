namespace AcadesArchitecturePattern.Shared.Commands
{
    public class GenericCommandResult : ICommandResult
    {
        public GenericCommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; } // true = success message || false = failure message
        public string Message { get; set; } // custom message to help front-end
        public Object Data { get; set; } // return an object
    }
}
