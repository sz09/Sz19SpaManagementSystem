
namespace SMGS.Presentation.CustomAttribute
{
    using System.Web.Http;

    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base() => Roles = string.Join(",", roles);
    }
}