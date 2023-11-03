namespace WebApiTemplate.SharedKernel.Enums
{
    public enum ErrorCode
    {
        ValidationError,
        ResourceNotFound,
        UnauthorizedAccess,
        AuthenticationFailed,
        DatabaseError,
        ConcurrencyConflict,
        RateLimitExceeded,
        InvalidInput,
        ServiceUnavailable,
        Timeout,
        Configuration,
        UnexpectedError
    }
}
