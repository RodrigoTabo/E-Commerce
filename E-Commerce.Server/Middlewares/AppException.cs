namespace E_Commerce.Server.Middlewares
{
    public abstract class AppException : Exception
    {
        public int StatusCode { get; }
        public string Title { get; }
        public string? Detail { get; }

        protected AppException(int statusCode, string title, string? detail = null)
            : base(detail ?? title)
        {
            StatusCode = statusCode;
            Title = title;
            Detail = detail;
        }
    }

    public sealed class BadRequestException : AppException
    {
        public BadRequestException(string title, string? detail = null)
            : base(StatusCodes.Status400BadRequest, title, detail) { }
    }

    public sealed class NotFoundException : AppException
    {
        public NotFoundException(string title, string? detail = null)
            : base(StatusCodes.Status404NotFound, title, detail) { }
    }

    public sealed class ConflictException : AppException
    {
        public ConflictException(string title, string? detail = null)
            : base(StatusCodes.Status409Conflict, title, detail) { }
    }

    public sealed class UnprocessableEntityException : AppException
    {
        public UnprocessableEntityException(string title, string? detail = null)
            : base(StatusCodes.Status422UnprocessableEntity, title, detail) { }
    }
}
