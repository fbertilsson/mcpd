using System.IdentityModel.Selectors;
using System.Security.Authentication;

namespace SecurityCustomUsernamePassword1
{
    internal class MyValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            var isAuthenticated =
                (userName == "Bond" && password == "James Bond");
            if (! isAuthenticated) throw new AuthenticationException("This is my own authentication exception.");
        }
    }
}