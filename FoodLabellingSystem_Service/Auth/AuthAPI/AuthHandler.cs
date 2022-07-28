using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace FoodLabellingSystem_Service.Auth.AuthAPI
{
    public class AuthHandler : AuthenticationHandler<AuthSchemeOptions>
    {
        public AuthHandler(IOptionsMonitor<AuthSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {

        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            TokenModel? model;

            if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                return Task.FromResult(AuthenticateResult.Fail("Header Not Found"));
            }
            // generated token from users info obtained from database
            // Delta is the role
            string generatedToken = "eyJVc2VyTmFtZSI6Im1nZGFuYSIsIkVtYWlsIjoibWdkYW5hQG91dGxvb2suY29tIn0=";

            var header = Request.Headers[HeaderNames.Authorization].ToString();
            var tokenMatch = Regex.Match(header, generatedToken);

            if (tokenMatch.Success)
            {

                var token = tokenMatch.Value;
                try
                {
                    // figure out how to convert back from base64 to json
                    var fromBase64String = Convert.FromBase64CharArray(token.ToArray(), 0, token.Length);
                    var parsedToken = Encoding.UTF8.GetString(fromBase64String);

                    // deserialize
                    model = JsonConvert.DeserializeObject<TokenModel>(parsedToken);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Occured while Deserializing: " + ex);
                    return Task.FromResult(AuthenticateResult.Fail("TokenParseException"));
                }

                if (model != null)
                {

                    var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, model.UserName),
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Name, model.UserName)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "Delta");
                    var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), this.Scheme.Name);
                    var re = Task.FromResult(AuthenticateResult.Success(ticket));
                    return re;
                }

            }
            return Task.FromResult(AuthenticateResult.Fail("Model is Empty"));
        }
    }
}
