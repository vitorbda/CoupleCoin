using System.ComponentModel.DataAnnotations.Schema;

namespace CoupleCoinApi.Models
{
    public class ValidateRegisterModel
    {
        [NotMapped]
        public bool Valid { get; set; }
        [NotMapped]
        public string Message { get; set; } = string.Empty;
        [NotMapped]
        public int? StatusCode { get; set; }
    }
}
