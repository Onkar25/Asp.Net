namespace API.Errors;
public class ApiExceptions(int statusCode, string message, string? details)
{
    public int statusCode { get; set; } = statusCode;
    public string message { get; set; } = message;
    public string? details { get; set; } = details;
}