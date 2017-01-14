using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WeddingSite.Data;
using WeddingSite.Data.Users;

namespace WeddingSite.Infrastructure
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtProvider
    {
        private readonly RequestDelegate _next;

        private TimeSpan _tokenExpiration;
        private readonly SigningCredentials _signingCredentials;
        private ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        //Values such as this need to be stored in a more secure location in production
        //e.g. environment variable or key management tool
        private const string PrivateKey = "private_key_1234567890";
        public static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(PrivateKey));
        public static readonly string Issuer = "WeddingSite";
        public static string TokenEndPoint = "/api/connect/token";


        public JwtProvider(RequestDelegate next, ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _next = next;

            _tokenExpiration = TimeSpan.FromMinutes(10);
            _signingCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if(!httpContext.Request.Path.Equals(TokenEndPoint, StringComparison.Ordinal))
                return _next(httpContext);

            if (httpContext.Request.Method.Equals("POST") && httpContext.Request.HasFormContentType)
            {
                return CreateToken(httpContext);
            }

            httpContext.Response.StatusCode = 400;
            return httpContext.Response.WriteAsync("Bad Request");
        }

        private async Task CreateToken(HttpContext httpContext)
        {
            try
            {
                var userName = httpContext.Request.Form["username"];
                var password = httpContext.Request.Form["password"];

                var user = await _userManager.FindByNameAsync(userName);

                if (user == null && userName.Contains("@"))
                {
                    user = await _userManager.FindByEmailAsync(userName);
                }

                var success = user != null && await _userManager.CheckPasswordAsync(user, password);

                if (success)
                {
                    var now = DateTime.UtcNow;

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Iss, Issuer),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString(),
                            ClaimValueTypes.Integer64),
                    };

                    var token = new JwtSecurityToken(
                        claims: claims,
                        notBefore: now,
                        expires: now.Add(_tokenExpiration),
                        signingCredentials: _signingCredentials
                    );

                    var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

                    var jwt = new
                    {
                        access_token = encodedToken,
                        expiration = (int) _tokenExpiration.TotalSeconds
                    };

                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(jwt));
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsync("Invalid username or password");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JwtProviderExtensions
    {
        public static IApplicationBuilder UseJwtProvider(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtProvider>();
        }
    }
}
