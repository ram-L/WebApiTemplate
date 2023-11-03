namespace WebApiTemplate.SharedKernel.Constants
{
    public static class UserClaimTypes
    {
        private const string Namespace = "http://schemas.example.com/identity/user/claims";

        public const string AccountType = $"{Namespace}/type";
        public const string UserId = $"{Namespace}/uid";
        public const string Username = $"{Namespace}/username";
        public const string Email = $"{Namespace}/email";
        public const string Roles = $"{Namespace}/roles";
        public const string Permmisions = $"{Namespace}/permisssions";
    }
    public static class ClientClaimTypes
    {
        private const string Namespace = "http://schemas.example.com/identity/client/claims";

        public const string AccountType = $"{Namespace}/type";
        public const string ClientType = $"{Namespace}/clienttype";
        public const string ClientId = $"{Namespace}/cid";
        public const string ClientName = $"{Namespace}/name";
        public const string Roles = $"{Namespace}/roles";
        public const string Permmisions = $"{Namespace}/permisssions";
    }
}
