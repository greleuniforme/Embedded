using System.ComponentModel;

namespace APIEmbedded.Models.Account.Role
{
    public enum RoleEnum
    {
        [Description("Normal")]
        Normal = 1,
        [Description("Doctor")]
        Doctor,
        [Description("Moderator")]
        Moderator,
        [Description("Administrator")]
        Administrator,
    }
}
