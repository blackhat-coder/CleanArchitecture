namespace Domain.Shared;

public class ErrorDetails
{
    public ErrorDetails(string code)
    {
        Code = code;
    }
    public string Code { get; private set; }

}