namespace CoupleCoinApi.DTO
{
    public class UserDTO
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? NewPassword { get; set; }
        public string? CPF { get; set; }
        public bool? IsActive { get; set; }

    }
}
