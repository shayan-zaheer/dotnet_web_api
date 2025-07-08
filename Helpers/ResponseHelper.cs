namespace Core_Web_API.Helpers
{
    public class ResponseHelper<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public ResponseHelper(bool success, string message, T? data)
        {
            this.Success = success;
            this.Message = message;
            this.Data = data;
        }

        public static ResponseHelper<T> SuccessResponse(T data, string? message = null)
        {
            return new ResponseHelper<T>(true, message ?? "Request successful", data);
        }

        public static ResponseHelper<T> FailureResponse(string? message = null)
        {
            return new ResponseHelper<T>(false, message ?? "Request failed", default);
        }
    }
}