namespace Doctorak.Server.Models;
public class ServiceResponse<T>
{
    public T Data { get; set; }
    public string Message { get; set; } = "Successful";
    public bool Success { get; set; } = true;
}
