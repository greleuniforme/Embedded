using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APIEmbedded.IdentityUserManager.Extension
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        public AppClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim(ClaimTypes.GivenName, user.FirstName)});
            }

            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                 new Claim(ClaimTypes.Surname, user.LastName)});
            }

            if (!string.IsNullOrWhiteSpace(user.Address))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                 new Claim(ClaimTypes.Locality, user.Address)});
            }

            if (!string.IsNullOrWhiteSpace(user.Gender))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                 new Claim(ClaimTypes.Gender, user.Gender)});
            }

            if (user.Birthday != null)
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                 new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToString())});
            }
            return principal;
        }
    }
}
