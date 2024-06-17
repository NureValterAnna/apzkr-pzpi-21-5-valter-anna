namespace Domain.Exceptions;

public class ApiException
{
    public ApiException(int statuCode, string message, string details)
    {
        StatuCode = statuCode;
        Message = message;
        Details = details;
    }

    public int StatuCode { get; set; }

    public string Message { get; set; }

    public string Details { get; set; }
}
