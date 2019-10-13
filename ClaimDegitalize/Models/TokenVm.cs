namespace ClaimDigitalize.Models
{
    public class TokenVM
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string ExpiresIn { get; set; }
    }
}