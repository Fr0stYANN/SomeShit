namespace Server.GraphQl
{
    public class TokenGenerationResponse
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
