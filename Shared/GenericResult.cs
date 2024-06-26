namespace Shared;

public class GenericResult<T>
{
    public string Message {get; set;} 
    public int StatusCode  {get; set;}
    public T? Data  {get; set;}
    public bool Status  {get; set;}

    public GenericResult()
    {
    }

    public GenericResult<T> Errored(string message, int statusCode)
    {
        Message = message;
        Status = false;
        StatusCode = statusCode;
        return this;
    }

    public GenericResult<T> Successed(string message, int statusCode, T data)
    {
        Message = message;
        Status = true;
        StatusCode = statusCode;
        Data = data;
        return this;
    }
}