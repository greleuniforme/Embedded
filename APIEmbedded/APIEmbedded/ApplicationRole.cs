using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace APIEmbedded
{
    public class ApplicationRole : IdentityRole
    {
        public async Task<IdentityResult> GenerateUserIdentityAsync(RoleManager<ApplicationRole> manager)
        {
            var roleIdentity = await manager.CreateAsync(this);
            return roleIdentity;
        }
    }
}
