using System.ComponentModel.DataAnnotations.Schema;

namespace CoupleCoinApi.Models.ViewModel
{
    public class UserViewModel
    {
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string? Email { get; set; }
        [NotMapped]
        public string? Name { get; set; }
        [NotMapped]
        public string? LastName { get; set; }
        [NotMapped]
        public string? Token { get; set; }
    }
}
