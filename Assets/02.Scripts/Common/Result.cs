public  class Result 
{
    public readonly bool IsSucces;
    public readonly string Message;

    public Result(bool isSucces, string message = "")
    {
        IsSucces = isSucces;
        Message = message;
    }
}
