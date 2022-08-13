using FoodLabellingSystem_Service.Auth.AuthMVC.Models;
using FoodLabellingSystem_Service.Auth.AuthMVC.Others;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace FoodLabellingSystem_Service.Auth.AuthMVC
{
    public class AuthMVC<TUserRepo> : CookieAuthenticationEvents, IServiceProvider  where TUserRepo : IUserRepo
    {
       
        private IHttpContextAccessor httpContext;
        protected AuthenticationProperties authenticationProperties;
        protected ClaimsPrincipal claimsPrincipal;
        protected List<Claim> claims;
        private IHashHelper _hashHelper;
       
        public TUserRepo dbUsers { get; set; }

        public AuthMVC(IHttpContextAccessor httpContextAccessor, IUserRepo userRepo, IHashHelper hashHelper)
        {
            claims = new List<Claim>();
            authenticationProperties = new AuthenticationProperties();
            httpContext = httpContextAccessor;
            claimsPrincipal = new ClaimsPrincipal();
            dbUsers = (TUserRepo)userRepo;
            _hashHelper = hashHelper;
        }

        public virtual void initClaimsPrincipal(IUser user)
        {      
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    claims.Add(new Claim("Status", user.Status.ToString()));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    claimsPrincipal.AddIdentity(claimsIdentity);
        }
        public virtual void setAuthenticationProperties(string redirectUrl)
        {
            authenticationProperties.AllowRefresh = true;
            authenticationProperties.IssuedUtc = DateTimeOffset.UtcNow;
            authenticationProperties.IsPersistent = true;
            authenticationProperties.RedirectUri = redirectUrl;
        }
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {

            var userPrincipal = context.Principal;

            // Looks for the Last changed claim.
            var currentStatus = (from c in userPrincipal?.Claims
                                 where c.Type == "Status"
                                 select c.Value).FirstOrDefault();

            var email = (from c in userPrincipal?.Claims
                         where c.Type == ClaimTypes.Email
                         select c.Value).FirstOrDefault();
            
            if (currentStatus != null && email != null)
            {
               IUser currentUser = dbUsers.FindByEmail(email);
                if (!(currentStatus == currentUser.Status.ToString() && currentStatus == StatusType.Activated.ToString()))
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
                if (httpContext.HttpContext != null) {
                    await httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);
                }
            }
        }

        public async Task logOutAsync()
        {
            if (httpContext.HttpContext != null) {
                await httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
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
