using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeddingSite.Data;
using WeddingSite.Data.Users;
using WeddingSite.ViewModels;

namespace WeddingSite.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : BaseController
    {
        public AccountsController(ApplicationDbContext dbContext, 
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager) : base(dbContext, signInManager, userManager)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var id = await GetCurrentUserId();
            var user = DbContext.Users.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                return new JsonResult(new UserViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    IsSocialLogin = user.IsSocialLogin
                }, DefaultJsonSettings);
            }

            return NotFound(new {Error = $"User Id {id} has not been found."});
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]UserViewModel userViewModel)
        {
            if (userViewModel != null)
            {
                try
                {
                    var user = await UserManager.FindByNameAsync(userViewModel.UserName);

                    if (user != null)
                    {
                        throw new Exception("UserName already exists");
                    }

                    user = await UserManager.FindByEmailAsync(userViewModel.Email);

                    if (user != null)
                    {
                        throw new Exception("Email already exists");
                    }

                    var now = DateTime.Now;

                    user = new ApplicationUser
                    {
                        UserName = userViewModel.UserName,
                        Email = userViewModel.Email,
                        DisplayName = userViewModel.DisplayName,
                        CreatedDate = now,
                        LastModifiedDate = now
                    };

                    var result = await UserManager.CreateAsync(user, userViewModel.Password);

                    if (!result.Succeeded)
                    {
                        var errors = string.Join(",", result.Errors.Select(x => x.Description));
                        throw new Exception($"Failed to create user: {errors}");
                    }

                    await UserManager.AddToRoleAsync(user, "Registered");

                    user.EmailConfirmed = true;
                    user.LockoutEnabled = false;

                    DbContext.SaveChanges();

                    return new JsonResult(new UserViewModel
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        DisplayName = user.DisplayName
                    }, DefaultJsonSettings);
                }
                catch (Exception exception)
                {
                    return new JsonResult(new {error = exception.Message});
                }
            }

            return new StatusCodeResult(500);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UserViewModel userViewModel)
        {
            if (userViewModel != null)
            {
                try
                {
                    var id = await GetCurrentUserId();

                    var user = await UserManager.FindByIdAsync(id);

                    if (user == null)
                    {
                        throw new Exception("User not found.");
                    }

                    if (await UserManager.CheckPasswordAsync(user, userViewModel.Password))
                    {
                        bool hadChanges = false;

                        if (user.Email != userViewModel.Email)
                        {
                            var user2 = await UserManager.FindByEmailAsync(userViewModel.Email);

                            if (user2 != null && user.Id != user2.Id)
                                throw new Exception("E-Mail already exists.");

                            await UserManager.SetEmailAsync(user, userViewModel.Email);
                            hadChanges = true;
                        }

                        if (!string.IsNullOrEmpty(userViewModel.PasswordNew))
                        {
                            await UserManager.ChangePasswordAsync(user, userViewModel.Password,
                                userViewModel.PasswordNew);
                            hadChanges = true;
                        }

                        if (user.DisplayName != userViewModel.DisplayName)
                        {   
                            hadChanges = true;
                        }

                        if (hadChanges)
                        {
                            user.LastModifiedDate = DateTime.Now;
                            DbContext.SaveChanges();
                        }

                        return new JsonResult(new UserViewModel
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            DisplayName = user.DisplayName
                        }, DefaultJsonSettings);
                    }

                    throw new Exception("Old password mismatch");
                }
                catch (Exception exception)
                {
                    return new JsonResult(new { error = exception.Message });
                }
            }

            return NotFound(new {error = "Current user has not been found"});
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(string id)
        {
            return BadRequest(new {error = "not implemented (yet)"});
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return BadRequest(new {Error = "not implemented (yet)"});
        }
        
        [HttpGet("ExternalLogin/{provider}")]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            switch (provider.ToLower())
            {
                case "facebook":
                case "google":
                case "twitter":
                    var redirectUrl = Url.Action("ExternalLoginCallback", "Accounts", new { ReturnUrl = returnUrl });
                    var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
                    return Challenge(properties, provider);
                default:
                    return BadRequest(new
                    {
                        Error = $"Provider {provider} is not supported"
                    });
            }
        }

        [HttpGet("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            try
            {
                if (remoteError != null)
                {
                    throw new Exception(remoteError);
                }

                var info = await SignInManager.GetExternalLoginInfoAsync();

                if (info == null)
                {
                    throw new Exception("ERROR: No login info available.");
                }

                var user = await UserManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                if (user == null)
                {
                    var emailKey = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
                    var email = info.Principal.FindFirst(emailKey)?.Value;

                    if(!string.IsNullOrEmpty(email))
                        user = await UserManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        var now = DateTime.Now;

                        var idKey = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

                        var username = $"{info.LoginProvider}{info.Principal.FindFirst(idKey)?.Value}";

                        user = new ApplicationUser
                        {
                            UserName = username,
                            Email = email,
                            CreatedDate = now,
                            LastModifiedDate = now
                        };

                        await UserManager.CreateAsync(user);

                        await UserManager.AddToRoleAsync(user, "Registered");

                        user.EmailConfirmed = true;
                        user.IsSocialLogin = true;
                        user.LockoutEnabled = false;
                    }

                    await UserManager.AddLoginAsync(user, info);

                    await DbContext.SaveChangesAsync();
                }

                var auth = new
                {
                    type = "External",
                    providerName = info.LoginProvider
                };

                return Content($"<script type='text/javascript' /> window.opener.externalProviderLogin({JsonConvert.SerializeObject(auth)}); window.close(); </script>", "text/html");
            }
            catch(Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                SignInManager.SignOutAsync().Wait();
            }

            return Ok();
        }
    }
}
