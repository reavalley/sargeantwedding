using BensWedding.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
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
            using (var dbContext = new ApplicationDbContext())
            {
                var partyMembers = dbContext.PartyMembers;

                var model = new PartyViewModel
                {
                    BridalParty = partyMembers.ToList()
                };

                return View(model);
            }
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

        [Authorize(Roles = "Admin")]
        public ActionResult ViewRsvps()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var options = dbContext.MenuOptions;
                var attendings = dbContext.AttendingWhen;

                var model = new List<RsvpDisplayViewModel>();

                var rsvps = dbContext.Rsvps
                    .Include(x => x.User)
                    .Include(x => x.Attending)
                    .Include(x => x.MenuOption);

                foreach (var rsvp in rsvps)
                {
                    var vm = new RsvpDisplayViewModel
                    {
                        Attending = rsvp.Attending.Description,
                        DietaryRequirements = rsvp.DietaryRequirements,
                        IsCamping = rsvp.IsCamping,
                        MenuOption = rsvp.MenuOption?.Description,
                        Name = rsvp.Name,
                        SongRequest = rsvp.SongRequest
                    };

                    model.Add(vm);
                }

                return View(model);
            }
        }

        [Authorize]
        public ActionResult Rsvp(int? id)
        {
            var userid = User.Identity.GetUserId();

            using (var dbContext = new ApplicationDbContext())
            {
                var options = dbContext.MenuOptions;
                var attendings = dbContext.AttendingWhen;

                var model = new RsvpViewModel
                {
                    MenuOptions = options.ToList(),
                    Attendings = attendings.ToList(),
                    Rsvps = new List<RsvpDisplayViewModel>()
                };

                var rsvps = dbContext.Rsvps
                    .Where(x => x.UserId == userid)
                    .Include(x => x.User)
                    .Include(x => x.Attending)
                    .Include(x => x.MenuOption)
                    .ToList();

                foreach (var rsvp in rsvps)
                {
                    var rsvpViewModel = new RsvpDisplayViewModel
                    {
                        Id = rsvp.Id,
                        Attending = rsvp.Attending.Description,
                        DietaryRequirements = rsvp.DietaryRequirements,
                        IsCamping = rsvp.IsCamping,
                        MenuOption = rsvp.MenuOption?.Description,
                        Name = rsvp.Name,
                        SongRequest = rsvp.SongRequest
                    };

                    if (rsvp.Id == id)
                    {
                        model.IsCamping = rsvp.IsCamping;
                        model.SelectedAttendingId = rsvp.Attending.Id;
                        model.SelectedMenuOptionId = rsvp.MenuOption?.Id;
                        model.DietaryRequirements = rsvp.DietaryRequirements;
                        model.ShowMenuOptions = rsvp.Attending?.Description == "Day";
                        model.Name = rsvp.Name;
                        model.SongRequest = rsvp.SongRequest;
                    }

                    model.Rsvps.Add(rsvpViewModel);
                }

                return View(model);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveRsvp(RsvpViewModel rsvpViewModel)
        {
            var userid = User.Identity.GetUserId();

            using (var dbContext = new ApplicationDbContext())
            {
                var attending = dbContext.AttendingWhen.SingleOrDefault(x => x.Id == rsvpViewModel.SelectedAttendingId);
                var menuOption = dbContext.MenuOptions.SingleOrDefault(x => x.Id == rsvpViewModel.SelectedMenuOptionId);

                var user = dbContext.Users.SingleOrDefault(x => x.Id == userid);

                var rsvp = dbContext.Rsvps.SingleOrDefault(x => x.Id == rsvpViewModel.Id);

                if (rsvp != null)
                {
                    rsvp.IsCamping = rsvpViewModel.IsCamping;
                    rsvp.DietaryRequirements = rsvpViewModel.DietaryRequirements;
                    rsvp.Attending = attending;
                    rsvp.MenuOption = menuOption;
                    rsvp.Name = rsvpViewModel.Name;
                    rsvp.SongRequest = rsvpViewModel.SongRequest;
                }
                else
                {
                    rsvp = new Rsvp
                    {
                        Attending = attending,
                        MenuOption = menuOption,
                        IsCamping = rsvpViewModel.IsCamping,
                        DietaryRequirements = rsvpViewModel.DietaryRequirements,
                        UserId = userid,
                        Name = rsvpViewModel.Name,
                        SongRequest = rsvpViewModel.SongRequest
                    };

                    dbContext.Rsvps.Add(rsvp);
                }
                dbContext.SaveChanges();

                return RedirectToAction("Rsvp");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRsvp(int id)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var rsvp = dbContext.Rsvps.SingleOrDefault(x => x.Id == id);

                if (rsvp != null)
                {
                    dbContext.Rsvps.Remove(rsvp);
                    dbContext.SaveChanges();
                }              

                return RedirectToAction("Rsvp");
            }
        }
    }
}