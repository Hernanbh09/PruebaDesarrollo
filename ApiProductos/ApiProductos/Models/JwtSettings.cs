namespace ApiProductos.Models
{
    public class JwtSettings
    {
        public string Key { get; set; }  
        public string Issuer { get; set; }
        public string Audience { get; set; }   
        public int ExpiresInMinutes { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
