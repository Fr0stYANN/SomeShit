namespace Server.GraphQl
{
    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
