using APIEmbedded.Config;
using APIEmbedded.Extensions;
using APIEmbedded.Models.Account.Role;
using Microsoft.AspNetCore.Identity;
using System;

namespace APIEmbedded.IdentityUserManager
{
    public interface IAddDefaultUser
    {
        void AddUserIfNotExistAsync(UserManager<ApplicationUser> userManager);
        void AddRoleIfNotExistAsync(RoleManager<ApplicationRole> roleManager);
    }

    public class AddDefaultUser : IAddDefaultUser
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AddDefaultUser(IConfiguration config, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _config = config;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void AddRoleIfNotExistAsync(RoleManager<ApplicationRole> roleManager)
        {
            int nbRole = Enum.GetNames(typeof(RoleEnum)).Length;
            foreach (RoleEnum current in Enum.GetValues(typeof(RoleEnum)))
            {
                if (!await roleManager.RoleExistsAsync(current.ToStringName()))
                {
                    ApplicationRole role = new ApplicationRole()
                    {
                        Name = current.ToStringName()
                    };
                    IdentityResult roleResult = await roleManager.CreateAsync(role);
                }
            }
        }

        public async void AddUserIfNotExistAsync(UserManager<ApplicationUser> userManager)
        {
            var confNewUser = new ApplicationUser()
            {
                Email = _config.Get<string>(ConfigurationKeys.AdminAccount.Email),
                UserName = _config.Get<string>(ConfigurationKeys.AdminAccount.Email),
                FirstName = _config.Get<string>(ConfigurationKeys.AdminAccount.FirstName),
                LastName = _config.Get<string>(ConfigurationKeys.AdminAccount.LastName),
                Address = "",
                Gender = "",
                Birthday = DateTime.UtcNow
            };
            var result = await userManager.CreateAsync(confNewUser, _config.Get<string>(ConfigurationKeys.AdminAccount.Password));
            if (result.Succeeded)
                await userManager.AddToRoleAsync(confNewUser, RoleEnum.Administrator.ToStringName());
        }
    }
}
