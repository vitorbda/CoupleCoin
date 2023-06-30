using System.ComponentModel.DataAnnotations.Schema;

namespace CoupleCoinApi.Models
{
    public class LoginModel
    {
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string Password { get; set; }
    }
}
