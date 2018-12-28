using APIEmbedded.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace APIEmbedded.Config
{
    public class ConfigurationKeys
    {
        public static SymmetricSecurityKey SigninKey { get; set; }
        public static TokenProviderOptions UserTokenOptions { get; set; }

        public static class Runtime
        {
            public static string Mode => "Runtime:Mode";
        }

        public static class Credentials
        {
            public static string Host => "ServiceManager:MongoDB:Ip";
            public static string Port => "ServiceManager:MongoDB:Port";
            public static string Database => "ServiceManager:MongoDB:Database";
            public static string DbUser => "ServiceManager:MongoDB:Username";
            public static string Password => "ServiceManager:MongoDB:Password";
        }

        public static class ConnectionStrings
        {
            public static string LocalDbPath => "ConnectionStrings:DefaultConnection";
        }

        public static class AdminAccount
        {
            public static string Role => "AdminAccount:Role";
            public static string Email => "AdminAccount:Email";
            public static string Password => "AdminAccount:Password";
            public static string FirstName => "AdminAccount:FirstName";
            public static string LastName => "AdminAccount:LastName";
        }
    }
}
