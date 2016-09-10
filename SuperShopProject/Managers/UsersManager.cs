using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using SuperShopProject.Models;

namespace SuperShopProject.Managers
{
    public class UsersStore : UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>
    {
        public UsersStore(ApplicationDbContext context) : base(context) { }
    }

    public class UsersManager: UserManager<User, Guid>
    {
        private readonly ApplicationDbContext _db;
        public UsersManager(ApplicationDbContext db, UsersStore store, IDataProtectionProvider dataProtectionProvider) : base(store)
        {
            _db = db;

            UserValidator = new UserValidator<User, Guid>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User, Guid>
            {
                MessageFormat = "Your security code is {0}"
            });

            RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User, Guid>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            UserTokenProvider = new DataProtectorTokenProvider<User, Guid>(dataProtectionProvider.Create("ASP.NET Identity"));
        }

        
    }

    public class ApplicationSignInManager : SignInManager<User, Guid>
    {
        public ApplicationSignInManager(UsersManager usersManager, IAuthenticationManager authenticationManager)
            : base(usersManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync(UserManager);
        }

        public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var user = UserManager.FindByName(userName);
            if (user != null)
            {
                return base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
            }
            return Task.FromResult(SignInStatus.Failure);
        }

    }
}