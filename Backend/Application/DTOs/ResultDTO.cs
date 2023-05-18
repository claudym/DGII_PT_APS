namespace DGIIAPP.Application.DTOs;

public class ResultDTO {
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;

    public bool IsSuccessful() => Success;
    public static ResultDTO Valid() => new() { Success = true };
    public static ResultDTO Invalid(string message) => new() { Success = false, Message = message };

}

public class ResultDTO<T> : ResultDTO where T: class {
    public T? Data { get; set; }

    public static ResultDTO<T> Valid(T resultObject) => new() { Success = true, Data = resultObject };
    public new static ResultDTO<T> Invalid(string message) => new() { Success = false, Message = message };
}
