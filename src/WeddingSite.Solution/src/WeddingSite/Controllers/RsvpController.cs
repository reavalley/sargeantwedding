using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Linq;
using WeddingSite.Data;
using WeddingSite.Data.Users;
using WeddingSite.ViewModels;

namespace WeddingSite.Controllers
{
    public class RsvpController : BaseController
    {
        public RsvpController(ApplicationDbContext dbContext, 
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager) : base(dbContext, signInManager, userManager)
        {
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var options = DbContext.MenuOptions.ToList();
            return new JsonResult(ToRsvpViewModel(options), DefaultJsonSettings);
        }

        private static RsvpViewModel ToRsvpViewModel(IEnumerable<MenuOption> menuOptions)
        {
            var options = menuOptions.Select(TinyMapper.Map<MenuOptionViewModel>);

            return new RsvpViewModel
            {
                Attending = new List<string> { "Day", "Evening" },
                MenuOptions = options
            };
        }
    }
}
