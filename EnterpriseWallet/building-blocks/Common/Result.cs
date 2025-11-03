namespace EnterpriseWallet.Common;

public class Result<T>
{
    private Result(bool isSuccessful, T? value, string message)
    {
        IsSuccessful = isSuccessful;
        Value = value;
        ErrorMessage = message;
    }

    public bool IsSuccessful { get; }
    public T? Value { get; }
    public string ErrorMessage { get; }

    public static Result<T> Success(T value) => new(true, value, string.Empty);
    public static Result<T> Failure(string message) => new(false, default, message);
}
