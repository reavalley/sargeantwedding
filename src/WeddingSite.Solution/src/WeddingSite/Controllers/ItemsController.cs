using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
using WeddingSite.Data;
using WeddingSite.Data.Items;
using WeddingSite.Data.Users;
using WeddingSite.ViewModels;

namespace WeddingSite.Controllers
{
    public class ItemsController : BaseController
    {
        public ItemsController(
            ApplicationDbContext dbContext, 
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager) : base(dbContext, signInManager, userManager)
        {
        }
        
        /// <summary>
        /// GET: api/items
        /// </summary>
        /// <returns>Nothing: this method will raise a HttpNotFound HTTp Exception, since we're not supporting this API call.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return NotFound(new {Error = "not found"});
        }

        /// <summary>
        /// GET: api/items/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Json-serialized object representing a single item.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = DbContext.Items.FirstOrDefault(x => x.Id == id);

            if (item != null)
                return new JsonResult(TinyMapper.Map<ItemvViewModel>(item), DefaultJsonSettings);

            return NotFound(new {Error = $"ItemID {id} has not been found"});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] ItemvViewModel itemViewModel)
        {
            if (itemViewModel != null)
            {
                var item = TinyMapper.Map<Item>(itemViewModel);

                item.CreatedDate = item.LastModifiedDate = DateTime.Now;

                item.UserId = await GetCurrentUserId();

                DbContext.Items.Add(item);
                DbContext.SaveChanges();
                return new JsonResult(TinyMapper.Map<ItemvViewModel>(item), DefaultJsonSettings);
            }
            return new StatusCodeResult(500);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] ItemvViewModel itemViewModel)
        {
            if (itemViewModel == null) return NotFound(new {Error = $"Item ID {id} has not been found"});

            var item = DbContext.Items.FirstOrDefault(x => x.Id == id);

            if (item == null) return NotFound(new {Error = $"Item ID {id} has not been found"});

            item.UserId = itemViewModel.UserId;
            item.Description = itemViewModel.Description;
            item.Flags = itemViewModel.Flags;
            item.Notes = itemViewModel.Notes;
            item.Text = itemViewModel.Text;
            item.Title = itemViewModel.Title;
            item.Type = itemViewModel.Type;
            item.LastModifiedDate = DateTime.Now;

            DbContext.SaveChanges();

            return new JsonResult(TinyMapper.Map<ItemvViewModel>(item), DefaultJsonSettings);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var item = DbContext.Items.FirstOrDefault(x => x.Id == id);

            if (item == null) return NotFound(new { Error = $"Item ID {id} has not been found" });

            DbContext.Items.Remove(item);

            DbContext.SaveChanges();

            return new OkResult();
        }

        [HttpGet("GetLatest")]
        public IActionResult GetLatest()
        {
            return GetLatest(DefaultNumberOfItems);
        }

        //GET api/items/GetLatest/{n}
        [HttpGet("GetLatest/{n}")]
        public IActionResult GetLatest(int n)
        {
            if (n > MaximumNumberOfItems)
            {
                n = MaximumNumberOfItems;
            }

            var items = DbContext.Items.OrderByDescending(i => i.CreatedDate).Take(n);
            return new JsonResult(ToItemViewModelList(items), DefaultJsonSettings);
        }
        
        [HttpGet("GetMostViewed")]
        public IActionResult GetMostViewed()
        {
            return GetMostViewed(DefaultNumberOfItems);
        }

        //GET api/items/GetMostViewed/{n}
        [HttpGet("GetMostViewed/{n}")]
        public IActionResult GetMostViewed(int n)
        {
            if (n > MaximumNumberOfItems)
            {
                n = MaximumNumberOfItems;
            }

            var items = DbContext.Items.OrderByDescending(i => i.ViewCount).Take(n);
            return new JsonResult(ToItemViewModelList(items), DefaultJsonSettings);
        }

        [HttpGet("GetRandom")]
        public IActionResult GetRandom()
        {
            return GetRandom(DefaultNumberOfItems);
        }

        //GET api/items/GetRandom/{n}
        [HttpGet("GetRandom/{n}")]
        public IActionResult GetRandom(int n)
        {
            if (n > MaximumNumberOfItems)
            {
                n = MaximumNumberOfItems;
            }

            var items = DbContext.Items.OrderByDescending(i => Guid.NewGuid()).Take(n);
            return new JsonResult(ToItemViewModelList(items), DefaultJsonSettings);
        }

        private static List<ItemvViewModel> ToItemViewModelList(IEnumerable<Item> items)
        {
            return items.Select(TinyMapper.Map<ItemvViewModel>).ToList();
        }
        
        private static int DefaultNumberOfItems => 5;

        private static int MaximumNumberOfItems => 100;
    }
}
