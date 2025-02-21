namespace AcadesArchitecturePattern.Shared.Queries
{
    public class GenericQueryResult(bool success, string message, object data) : IQueryResult
    {
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
        public Object Data { get; set; } = data;
    }
}
