using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using APIEmbedded.Account.Models.Token;
using APIEmbedded.DTO;
using APIEmbedded.Extensions;
using APIEmbedded.Models.Account.Login;
using APIEmbedded.Models.Account.Register;
using APIEmbedded.Models.Account.Role;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APIEmbedded.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Auth")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/account/login")]
        [ProducesResponseType(typeof(TokenUserModel), 200)]
        public async Task<IActionResult> Login([FromBody]LoginUserModel userLog)
        {
            if (CurrentUserIsLogged())
                return BadRequest("already logged");
            var res = await ConnectAndGenerateToken(userLog);
            if (res == null)
                return StatusCode(401);
            return Ok(res);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/account/register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserModel newUser)
        {
            if (CurrentUserIsLogged())
                return BadRequest("disconnected for create new account");
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfile<DtoProfile>());
            var confNewUser = Mapper.Map<ApplicationUser>(newUser);

            if (!(await RegisterMethod(confNewUser, newUser.Password, RoleEnum.Normal)))
                return BadRequest();
            return Ok();
        }


        [HttpGet]
        [Authorize]
        [Route("/api/account/islogged")]
        public IActionResult IsLogged()
        {
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("/api/account/role")]
        public async Task<IActionResult> Role()
        {
            return Ok(await CurrentUserRole());
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("/api/account/listUsers")]
        [ProducesResponseType(typeof(List<string>), 200)]
        public async Task<IActionResult> Users()
        {
            var user = _signInManager.UserManager.Users.ToList();
            
            return Ok(user.Select(c => c.Email).ToList());
        }

        private object GenerateToken(List<Claim> claims)
        {
            try
            {
                if (claims != null)
                {
                    DateTime now = DateTime.UtcNow;
                    var token = new JwtSecurityToken(
                        issuer: Config.ConfigurationKeys.UserTokenOptions.Issuer,
                        audience: Config.ConfigurationKeys.UserTokenOptions.Audience,
                        claims: claims,
                        expires: now.Add(Config.ConfigurationKeys.UserTokenOptions.AccessExpiration),
                        signingCredentials: Config.ConfigurationKeys.UserTokenOptions.SigningCredentials);

                    var refresh_token = new JwtSecurityToken(
                        issuer: Config.ConfigurationKeys.UserTokenOptions.Issuer,
                        audience: Config.ConfigurationKeys.UserTokenOptions.Audience,
                        claims: claims,
                        expires: now.Add(Config.ConfigurationKeys.UserTokenOptions.RefreshExpiration),
                        signingCredentials: Config.ConfigurationKeys.UserTokenOptions.SigningCredentials);

                    return new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        refresh_token = new JwtSecurityTokenHandler().WriteToken(refresh_token)
                    };
                }
            }
            catch (Exception e) //TODO CDS: a la place throw et check si une exception 
            {
            }
            return null;
        }

        private async Task<List<Claim>> GetClaimsIdentityWithEmailAndPAssword(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return await Task.FromResult<List<Claim>>(null);

            var userToVerify = await _signInManager.UserManager.FindByEmailAsync(email);
            if (userToVerify == null)
                return await Task.FromResult<List<Claim>>(null);

            if (await _signInManager.UserManager.CheckPasswordAsync(userToVerify, password))
            {
                var claimsPrincipal = await _signInManager.ClaimsFactory.CreateAsync(userToVerify);
                var claims = claimsPrincipal.Claims;
                return claims.ToList();
            }
            return await Task.FromResult<List<Claim>>(null);
        }

        private bool CurrentUserIsLogged()
        {
            return _signInManager.Context.User.Identity.IsAuthenticated;
        }

        private async Task<string> CurrentUserRole()
        {
            var roles = await _signInManager.UserManager.GetRolesAsync(await _signInManager.UserManager.GetUserAsync(_signInManager.Context.User));
            return roles.FirstOrDefault();
        }

        private async Task<object> ConnectAndGenerateToken(LoginUserModel userLog)
        {
            var result = await Connect(userLog);
            var identity = await GetClaimsIdentityWithEmailAndPAssword(userLog.Email, userLog.Password);
            if (result && identity != null)
                return GenerateToken(identity);
            return null;
        }

        private async Task<bool> RegisterMethod(ApplicationUser user, string password, RoleEnum roleEnum)
        {
            var accountCreatedRes = await RegisterUser(user, password);
            if (!accountCreatedRes)
                return false;
            await _signInManager.UserManager.AddToRoleAsync(user, roleEnum.ToStringName());
            if (!(await CheckUserExistAsync(user.Email)))
                return false;
            return true;
        }

        public async Task<bool> Connect(LoginUserModel userLog)
        {
            return (await _signInManager.PasswordSignInAsync(userLog.Email, userLog.Password, userLog.RememberMe, false)).Succeeded;
        }

        private async Task<bool> RegisterUser(ApplicationUser appUser, string password)
        {
            return (await _signInManager.UserManager.CreateAsync(appUser, password)).Succeeded;
        }

        public async Task<bool> CheckUserExistAsync(string email)
        {
            return ((await _signInManager.UserManager.FindByEmailAsync(email)) != null);
        }
    }
}
