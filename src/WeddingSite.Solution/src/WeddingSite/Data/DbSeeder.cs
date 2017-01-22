using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WeddingSite.Data.Comments;
using WeddingSite.Data.Items;
using WeddingSite.Data.Users;

namespace WeddingSite.Data
{
    public class DbSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public DbSeeder(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _dbContext.Database.EnsureCreated();

            if (await _dbContext.Users.CountAsync() == 0)
                await CreateUsersAsync();

            if (await _dbContext.Items.CountAsync() == 0)
                CreateItems();
        }

        private void CreateItems()
        {
            var createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            var lastModifiedDate = DateTime.Now;

            var authorId = _dbContext.Users.FirstOrDefault(x => x.UserName == "Admin")?.Id;

#if DEBUG
            var num = 1000;

            for (int id = 1; id <= num; id++)
            {
                _dbContext.Items.Add(GetSampleItem(id, authorId, num - id, new DateTime(2015, 12, 31).AddDays(-num)));
            }
#endif
            EntityEntry<Item> e1 = _dbContext.Items.Add(new Item
            {
                UserId = authorId,
                Title = "Magarena",
                Description = "Single-player fantasy card game similar to Magic: The Gathering",
                Text =
                    @"Loosely based on Magic: The Gathering, the game lets you play against a computer opponent or another human being. The game features a well-developed AI, an intuitive and clear interface and an enticing level gameplay.",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = 2343,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            EntityEntry<Item> e2 = _dbContext.Items.Add(new Item
            {
                UserId = authorId,
                Title = "Minetest",
                Description = "Open-Source alternative to Minecraft",
                Text =
                    @"The Minetest gameplay is very similar to Minecraft's: you are playing in a 3D open world, where you can create and/or remove various types of blocks. Minetest features both single-player and multi-player games modes. It also has support for custom mods, additional texture packs and other custom/personalization options.",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = 2343,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            EntityEntry<Item> e3 = _dbContext.Items.Add(new Item
            {
                UserId = authorId,
                Title = "Relic Hunters Zero",
                Description = "A free game about shooting evil space ducks with tiny, cute guns.",
                Text =
                    @"Relic Hunters Zero is fast, tactical and also very smooth to play. It also enables the users to look at the source code, so they can get creative and keep this game alive, fun and free for years to come. The game is also available on Steam.",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = 5203,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            EntityEntry<Item> e4 = _dbContext.Items.Add(new Item
            {
                UserId = authorId,
                Title = "SuperTux",
                Description = "A classic 2D jump and run, side-scrolling game similar to the Mario series.",
                Text =
                    @"The game is currently under Milestone 3. The Milestone 2, wich is currently out, features the following: - a nearly completely rewritten game engine based on OpenGL, OpenAL, SDL2, ... - support for translations - in-game manager for downloadable add-ons and translations - Bonus Island III, a for now unfinished Forest Island and the development levels in Incubator Island - a final boss in Icy Island - new and improved soundtracks and sound effects ... and much more! The game has been released under the GNU GPL license.",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = 9602,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            EntityEntry<Item> e5 = _dbContext.Items.Add(new Item
            {
                UserId = authorId,
                Title = "Scrabble3D",
                Description = "A 3D-based revamp to the classic Scrable game.",
                Text =
                    @"Scrabble 3D extends the gameplay of the classic game Scrablle by adding a whole third dimension. Other than playing left to right or top to bottom, you'll be able to place your tiles above or beyond other tiles. Since the game features more fields, it also uses a larger letter set. You can either play against the computer, players from your LAN or from the Internet. The game also features a set of game servers where you can challenge players from all over the world and get ranked into an official, ELO-based rating/ladder system.",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = 6754,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            if (!_dbContext.Comments.Any())
            {
                int numComments = 10;

                for (int i = 1; i <= numComments; i++)
                {
                    _dbContext.Comments.Add(GetSampleComment(i, e1.Entity.Id, authorId, createdDate.AddDays(i)));
                }

                for (int i = 1; i <= numComments; i++)
                {
                    _dbContext.Comments.Add(GetSampleComment(i, e2.Entity.Id, authorId, createdDate.AddDays(i)));
                }

                for (int i = 1; i <= numComments; i++)
                {
                    _dbContext.Comments.Add(GetSampleComment(i, e3.Entity.Id, authorId, createdDate.AddDays(i)));
                }

                for (int i = 1; i <= numComments; i++)
                {
                    _dbContext.Comments.Add(GetSampleComment(i, e4.Entity.Id, authorId, createdDate.AddDays(i)));
                }

                for (int i = 1; i <= numComments; i++)
                {
                    _dbContext.Comments.Add(GetSampleComment(i, e5.Entity.Id, authorId, createdDate.AddDays(i)));
                }
                _dbContext.SaveChanges();
            }
        }

        private Comment GetSampleComment(int n, int itemId, string authorId, DateTime createdDate)
        {
            return new Comment
            {
                ItemId = itemId,
                UserId = authorId,
                ParentId = null,
                Text = $"Sample comment {n} for the Item #{itemId}",
                CreatedDate = createdDate,
                LastModifiedDate = createdDate
            };
        }

        private Item GetSampleItem(int id, string authorId, int viewCount, DateTime createdDate)
        {
            return new Item
            {
                UserId = authorId,
                Title = $"Item {id} Title",
                Description = $"This is a sample description for item {id}: Lorem ipsum dolor sit amet.",
                Notes = "This is a sample record create by the Code-First Configuration class",
                ViewCount = viewCount,
                CreatedDate = createdDate,
                LastModifiedDate = createdDate
            };
        }

        private async Task CreateUsersAsync()
        {
            var createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            var lastModifiedDate = DateTime.Now;
            var roleAdministrators = "Administrators";
            var roleRegistered = "Registered";

            if(!await _roleManager.RoleExistsAsync(roleAdministrators))
                await _roleManager.CreateAsync(new IdentityRole(roleAdministrators));

            if (!await _roleManager.RoleExistsAsync(roleRegistered))
                await _roleManager.CreateAsync(new IdentityRole(roleRegistered));

            var userAdmin = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "admin@weddingsite.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            if (await _userManager.FindByIdAsync(userAdmin.Id) == null)
            {
                await _userManager.CreateAsync(userAdmin, "Pass4Admin");
                await _userManager.AddToRoleAsync(userAdmin, roleAdministrators);
                userAdmin.EmailConfirmed = true;
                userAdmin.LockoutEnabled = false;
            }
            
#if DEBUG
            var userRuss = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Russ",
                Email = "russ@weddingsite.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            var userSarah = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Sarah",
                Email = "sarah@weddingsite.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            var userAimee = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Aimee",
                Email = "aimee@weddingsite.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            if (await _userManager.FindByIdAsync(userRuss.Id) == null)
            {
                await _userManager.CreateAsync(userRuss, "Pass4Russ");
                await _userManager.AddToRoleAsync(userRuss, roleRegistered);
                userAdmin.EmailConfirmed = true;
                userAdmin.LockoutEnabled = false;
            }

            if (await _userManager.FindByIdAsync(userSarah.Id) == null)
            {
                await _userManager.CreateAsync(userSarah, "Pass4Sarah");
                await _userManager.AddToRoleAsync(userSarah, roleRegistered);
                userAdmin.EmailConfirmed = true;
                userAdmin.LockoutEnabled = false;
            }

            if (await _userManager.FindByIdAsync(userAimee.Id) == null)
            {
                await _userManager.CreateAsync(userAimee, "Pass4Aimee");
                await _userManager.AddToRoleAsync(userAimee, roleRegistered);
                userAdmin.EmailConfirmed = true;
                userAdmin.LockoutEnabled = false;
            }

            await _dbContext.SaveChangesAsync();
#endif
        }
    }
}