namespace CoupleCoinApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string? CPF { get; set; }
        public string Role { get; set; }
        public string? Gender { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsActive { get; set; }        
    }
}
