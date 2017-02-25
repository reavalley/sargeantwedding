using BensWedding.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BensWedding.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;
        
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Itinery()
        {
            return View();
        }

        public ActionResult OurStory()
        {
            return View();
        }

        public ActionResult WeddingWish()
        {
            return View();
        }

        public ActionResult BridalParty()
        {
            return View();
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult Directions()
        {
            return View();
        }

        public ActionResult Accommodation()
        {
            return View();
        }

        [Authorize]
        public ActionResult Rsvp()
        {
            var userid = User.Identity.GetUserId();

            using (var dbContext = new ApplicationDbContext())
            {
                var options = dbContext.MenuOptions;
                var attendings = dbContext.AttendingWhen;

                var model = new RsvpViewModel
                {
                    MenuOptions = options.ToList(),
                    Attendings = attendings.ToList()
                };

                var rsvp = dbContext.Rsvps.SingleOrDefault(x => x.UserId == userid);

                if(rsvp != null)
                {
                    model.IsCamping = rsvp.IsCamping;
                    model.SelectedAttendingId = rsvp.Attending.Id;
                    model.SelectedMenuOptionId = rsvp.MenuOption.Id;
                }
                               
                return View(model);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult SaveRsvp(RsvpViewModel rsvpViewModel)
        {
            var userid = User.Identity.GetUserId();

            using (var dbContext = new ApplicationDbContext())
            {
                var attending = dbContext.AttendingWhen.SingleOrDefault(x => x.Id == rsvpViewModel.SelectedAttendingId);
                var menuOption = dbContext.MenuOptions.SingleOrDefault(x => x.Id == rsvpViewModel.SelectedMenuOptionId);

                var user = dbContext.Users.SingleOrDefault(x => x.Id == userid);
                
                if (attending == null || menuOption == null) return RedirectToAction("Index");

                var rsvp = dbContext.Rsvps.SingleOrDefault(x => x.UserId == userid);

                if (rsvp != null)
                {
                    rsvp.IsCamping = rsvpViewModel.IsCamping;
                    rsvp.Attending = attending;
                    rsvp.MenuOption = menuOption;
                }
                else
                {
                    rsvp = new Rsvp
                    {
                        Attending = attending,
                        MenuOption = menuOption,
                        IsCamping = rsvpViewModel.IsCamping,
                        UserId = userid
                    };

                    dbContext.Rsvps.Add(rsvp);
                }                  
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}