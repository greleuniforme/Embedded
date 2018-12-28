using System;
using System.Text;
using APIEmbedded.Config;
using APIEmbedded.IdentityUserManager;
using APIEmbedded.IdentityUserManager.Extension;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

// pour changer des champs register ajouter appclaimsprincipalfactory puis applicationsuser et les models

// lancer add-migration embedded puis update-database dans package manager pour init la db
namespace APIEmbedded
{
    public class Startup
    {
        public Autofac.IContainer ApplicationContainer { get; private set; }
        public Microsoft.Extensions.Configuration.IConfiguration ConfApp { get; }
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            ConfApp = configuration;

            ConfigurationKeys.SigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["TokenSettings:UserAuthSecretKey"]));
            ConfigurationKeys.UserTokenOptions = new Helpers.TokenProviderOptions
            {
                Audience = Configuration["TokenSettings:Issuer"],
                Issuer = Configuration["TokenSettings:Audience"],
                SigningCredentials = new SigningCredentials(ConfigurationKeys.SigninKey, SecurityAlgorithms.HmacSha256),
            };
        }

        public void ConfigureJwtAuthService(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = ConfigurationKeys.SigninKey,

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["TokenSettings:Issuer"],

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = Configuration["TokenSettings:Audience"],

                    // Validate the token expiry
                    ValidateLifetime = true,

                    // If you want to allow a certain amount of clock drift, set that here:
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddAuthorization(auth =>
                auth.AddPolicy("Auth", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build())
            );
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Auth",
                    builders => builders
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .Build());
            });

            ConfigureJwtAuthService(services);
            services.AddMvc();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(ConfApp.GetConnectionString("LocaldbPath")));
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            var builder = new ContainerBuilder();
            builder.RegisterModule(new DependencyModule());
            builder.Populate(services);
            this.ApplicationContainer = builder.Build();
            return (ApplicationContainer.Resolve<IServiceProvider>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IAddDefaultUser addAdminUser,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            app.UseCors("Auth");
            app.UseAuthentication();

            addAdminUser.AddRoleIfNotExistAsync(roleManager);
            addAdminUser.AddUserIfNotExistAsync(userManager);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
