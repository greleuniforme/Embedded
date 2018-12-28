using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace APIEmbedded
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<IdentityResult> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateAsync(this);
            return userIdentity;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
    }
}
