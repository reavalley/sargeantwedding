using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeddingSite.Data;
using WeddingSite.Data.Users;

namespace WeddingSite.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        protected ApplicationDbContext DbContext;
        protected SignInManager<ApplicationUser> SignInManager;
        protected UserManager<ApplicationUser> UserManager;

        public BaseController(ApplicationDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            DbContext = dbContext;
            SignInManager = signInManager;
            UserManager = userManager;   
        }

        public async Task<string> GetCurrentUserId()
        {
            if (!User.Identity.IsAuthenticated)
                throw new NotSupportedException();

            var info = await SignInManager.GetExternalLoginInfoAsync();

            if(info == null)
            {
                return User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                var user = await UserManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                if (user == null)
                    throw new NotSupportedException();

                return user.Id;
            }
        }

        protected JsonSerializerSettings DefaultJsonSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            }
        }
    }
}
