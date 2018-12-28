using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace APIEmbedded.Models.Account.Register
{
    public class RegisterUserModel
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
        [Display(Name = "firstname")]
        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "lastname")]
        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "address")]
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "birthday")]
        [JsonProperty(PropertyName = "birthday")]
        public DateTime Birthday { get; set; }

        [Required]
        [Display(Name = "gender")]
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }
    }
}