using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace APIEmbedded.Models.Account.Login
{
    public class LoginUserModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "email")]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "rememberMe")]
        [JsonProperty(PropertyName = "rememberMe")]
        public bool RememberMe { get; set; }
    }
}
