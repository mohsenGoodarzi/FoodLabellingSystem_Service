using FoodLabellingSystem_Service.Auth.AuthMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace FoodLabellingSystem_Service.Auth.AuthMVC
{
    public class AuthMVC<TUser, TUserRepo> : CookieAuthenticationEvents, IServiceProvider where TUser : IUser, new() where TUserRepo : IUserRepo
    {
        private TUser? currentUser;
        private IHttpContextAccessor httpContext;
        protected AuthenticationProperties authenticationProperties;
        protected ClaimsPrincipal claimsPrincipal;
        protected List<Claim> claims;

        public TUser? user { get; set; }
        public TUserRepo dbUsers { get; set; }

        public AuthMVC(IHttpContextAccessor httpContextAccessor, IUserRepo userRepo)
        {
            claims = new List<Claim>();
            authenticationProperties = new AuthenticationProperties();
            httpContext = httpContextAccessor;
            claimsPrincipal = new ClaimsPrincipal();
            dbUsers = (TUserRepo)userRepo;
        }

        public virtual void initClaimsPrincipal(TUser user)
        {

            if (user != null)
            {
                currentUser = (TUser?)(from usr in dbUsers.Users where usr.Email == user.Email && usr.Password == user.Password select usr).FirstOrDefault();
            }
            if (currentUser != null)
            {
                if (currentUser.Status.Equals(StatusType.Activated) ||
                    currentUser.Status.Equals(StatusType.Registered) ||
                    currentUser.Status.Equals(StatusType.Await_Email_Confirmation) ||
                    currentUser.Status.Equals(StatusType.Await_Phone_Confimation))
                {

                    claims.Add(new Claim(ClaimTypes.Name, currentUser.UserName));
                    claims.Add(new Claim(ClaimTypes.Role, currentUser.Role.ToString()));
                    claims.Add(new Claim(ClaimTypes.Email, currentUser.Email));
                    claims.Add(new Claim("Status", currentUser.Status.ToString()));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    claimsPrincipal.AddIdentity(claimsIdentity);
                }
            }

        }
        public virtual void setAuthenticationProperties(/*DateTimeOffset dateTimeOffset, */ string redirectUrl)
        {
              //refereshes the Authentication session
             // ExpiresUtc = dateTimeOffset, //DateTimeOffset.UtcNow.AddSeconds(20), // the expiery time for the Cookie

            authenticationProperties.AllowRefresh = true;
            authenticationProperties.IssuedUtc = DateTimeOffset.UtcNow;
            authenticationProperties.IsPersistent = true;
            authenticationProperties.RedirectUri = redirectUrl;
        }
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {


            var userPrincipal = context.Principal;

            // Look for the LastChanged claim.
            var currentStatus = (from c in userPrincipal?.Claims
                                 where c.Type == "Status"
                                 select c.Value).FirstOrDefault();

            var email = (from c in userPrincipal?.Claims
                         where c.Type == ClaimTypes.Email
                         select c.Value).FirstOrDefault();

            // you can check it here with db context to see if the role is same as claim or something else
            // this can apply over user name.
            // we need to make sure that as long as the cookie exist the account is active username and roles are valid too
            if (currentStatus != null && email != null)
            {
                currentUser = (TUser?)dbUsers.FindByEmail(email);
                if ((currentUser != null && currentStatus != currentUser.Status.ToString()) || currentUser == null)
                {
                    context.RejectPrincipal();

                    await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            }
        }
        public async Task logInAsync()
        {

            if (claimsPrincipal.Identity != null)
            {
                if (httpContext.HttpContext != null)
                    await httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

            }

        }

        public async Task logOutAsync()
        {
            if (httpContext.HttpContext != null)
                await httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        }

        public AuthenticationProperties getAuthenticationProperties()
        {
            return authenticationProperties;
        }

        public object? GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
