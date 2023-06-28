namespace CoupleCoinApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; } = string.Empty;
        public string Password { get; set; }
        public string PasswordConfirmed { get; set; } = string.Empty;
        public string? CPF { get; set; }
        public string Gender { get; set; }
        public bool HasPair { get; set; } = false;
        public bool IsActive { get; set; }        
    }
}
