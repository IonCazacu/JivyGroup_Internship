using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using server.Services.UserServices.Model;
using server.Services.UserServices.Ports;

namespace server.Services.UserServices.Authorization.Basic
{
    public class AuthorizationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;

        public AuthorizationHandler (
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService userService)
            : base (options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // No authorization header, return fail
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            try
            {
                var authenticationHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                
                // If authorization header doesn't contain basic, return fail
                if (authenticationHeader.Scheme != "Basic")
                {
                    return AuthenticateResult.Fail("Invalid authorization scheme");
                }

                var parameter = authenticationHeader.Parameter;
                if (string.IsNullOrEmpty(parameter))
                {
                    return AuthenticateResult.Fail("Username or password cannot be empty");
                }

                // Decrypt the authorization header and split out the client
                // username/password which is separate by the first ':'
                var credentials = Encoding.UTF8.GetString(
                    Convert.FromBase64String (parameter)).Split(':', 2);
                if (credentials.Length != 2)
                {
                    return AuthenticateResult.Fail("Invalid credentials format");
                }

                // Store the client usernae and password
                string username = credentials[0];
                string password = credentials[1];

                // Authenticate the user
                User? user = await _userService.Login(username, password);
                if (user == null)
                {
                    return AuthenticateResult.Fail("Username or password is invalid");
                }

                // claims - an array of Claim objects
                // Each Claim object represents a specific piece of information
                // about a user
                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username!)
                };

                var identity = new ClaimsIdentity (claims, Scheme.Name);
                var pricipal = new ClaimsPrincipal (identity);
                var ticket = new AuthenticationTicket (pricipal, Scheme.Name);

                return AuthenticateResult.Success (ticket);
            }
            catch (Exception e)
            {
                return AuthenticateResult.Fail (e.Message);
            }
        }
    }
}
