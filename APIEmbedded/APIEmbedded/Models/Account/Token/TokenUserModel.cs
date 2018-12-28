using System.ComponentModel.DataAnnotations;

namespace APIEmbedded.Account.Models.Token
{
    public class TokenUserModel
    {
        [Required]
        [Display(Name = "token")]
        public string Token { get; set; }
    }
}   