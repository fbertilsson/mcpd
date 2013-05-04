using System;
using System.Security.Permissions;
using System.ServiceModel;
using System.Threading;

namespace security
{
    class MySecureService : IMySecureService
    {
        //[PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        [PrincipalPermission(SecurityAction.Demand, Name = "AFAB\\nofreber")]
        public void OnlyForAfabFredrik()
        {
            var identity = ServiceSecurityContext.Current.WindowsIdentity;
            Console.WriteLine("Good morning, {0}.", identity.Name);
        }

        //[PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public bool ReturnYesIfFredrik()
        {
            var identity = ServiceSecurityContext.Current.WindowsIdentity;
            var isFredrik = identity.Name.Equals("AFAB\\nofreber");
            Console.WriteLine("Good morning, {0}. isFredrik={1}", identity.Name, isFredrik);
            return isFredrik;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Users")]
        //Don't use the machine prefix. Does not work: [PrincipalPermission(SecurityAction.Demand, Role = "NOFREBER\\Users")]
        public void OnlyForUsers()
        {
            const string roleName = "Users";
            var isInRole = Thread.CurrentPrincipal.IsInRole(roleName);
            Console.WriteLine("is in role {0}: {1}", roleName, isInRole);        
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Administrators")]
        //Don't use the machine prefix. Does not work: [PrincipalPermission(SecurityAction.Demand, Role = "NOFREBER\\Administrators")]
        public void OnlyForAdmins()
        {
            const string roleName = "Administrators";
            var isInRole = Thread.CurrentPrincipal.IsInRole(roleName);
            Console.WriteLine("is '{0}' in role {1}: {2}", Thread.CurrentPrincipal.Identity.Name, roleName, isInRole);
        }
    }
}
