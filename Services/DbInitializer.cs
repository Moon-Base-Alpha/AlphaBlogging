using AlphaBlogging.Data;
using AlphaBlogging.Models;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace AlphaBlogging.Services
{
    public class DbInitializer : IAsyncInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly bool InitializeDb = false;


        //What to add
        //private readonly bool Add_Roles = true;
        //private readonly bool Add_Blogs = true;

        public DbInitializer(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // CreateRole Method
        private async Task CreateRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole
                {
                    Name = roleName
                };
                await _roleManager.CreateAsync(role);
            }
        }

        public async Task InitializeAsync()
        {
            //Creates the database if it does not exit and applies any pending migrations
            await _db.Database.MigrateAsync();

            //if true, initializes the database with some sample data
            if (InitializeDb)
            {
                    await CreateRoleAsync("Admin");
                    await CreateRoleAsync("User");
                    await CreateRoleAsync("Author");
                    //await CreateRoleAsync("SuperUser");


            //Seed users

            if (_userManager.FindByEmailAsync("admin@email.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Tony",
                    LastName = "Stark",
                    UserName = "admin",
                    Email = "admin@email.com",
                };
                await _userManager.CreateAsync(user, "123456");
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            if (_userManager.FindByEmailAsync("neo@email.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Neo",
                    LastName = "Andersson",
                    UserName = "Neo",
                    Email = "neo@email.com",
                };
                await _userManager.CreateAsync(user, "123456");
                await _userManager.AddToRoleAsync(user, "Author");
            }

            if (_userManager.FindByEmailAsync("mary@email.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Mary",
                    LastName = "Watson",
                    UserName = "MaryJ",
                    Email = "mary@email.com",
                };
                await _userManager.CreateAsync(user, "123456");
                await _userManager.AddToRoleAsync(user, "Author");
            }

            if (_userManager.FindByEmailAsync("lisa@email.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Lisa",
                    LastName = "Simpson",
                    UserName = "Lisa",
                    Email = "lisa@email.com",
                };
                await _userManager.CreateAsync(user, "123456");
                await _userManager.AddToRoleAsync(user, "Author");
            }

            if (_userManager.FindByEmailAsync("jack@email.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Jack",
                    LastName = "Frost",
                    UserName = "Jack",
                    Email = "jack@email.com",
                };
                await _userManager.CreateAsync(user, "123456");
                await _userManager.AddToRoleAsync(user, "User");
            }

            if (_userManager.FindByEmailAsync("peter@email.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Peter",
                    LastName = "Parker",
                    UserName = "Peter",
                    Email = "peter@email.com",
                };
                await _userManager.CreateAsync(user, "123456");
                await _userManager.AddToRoleAsync(user, "Author");
            }

            if (_userManager.FindByEmailAsync("clark@email.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Clark",
                    LastName = "Kent",
                    UserName = "Clark",
                    Email = "clark@email.com",
                };
                await _userManager.CreateAsync(user, "123456");
                await _userManager.AddToRoleAsync(user, "Author");
            }

            if (_userManager.FindByEmailAsync("diana@email.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Diana",
                    LastName = "Prince",
                    UserName = "Diana",
                    Email = "diana@email.com",
                };
                await _userManager.CreateAsync(user, "123456");
                await _userManager.AddToRoleAsync(user, "Author");
            }

            if (_userManager.FindByEmailAsync("bruce@email.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Bruce",
                    LastName = "Wayne",
                    UserName = "Bruce",
                    Email = "bruce@email.com",
                };
                await _userManager.CreateAsync(user, "123456");
                await _userManager.AddToRoleAsync(user, "User");
            }
            }
            // End seed users //

            }
        }



            // Seed Blogs
            if (_db.Blogs.Any())
            {
                return; // DB has been seeded
            }
            var Blogs = new Blog[]
            {
                    new Blog
                    {
                        Author = _db.Users.Where(x => x.UserName == "Neo").FirstOrDefault(),
                        Title = "What is real? How do you define real?",
                        Body = "What you know you can't explain, but you feel it. You've felt it your entire life, " +
                        "that there's something wrong with the world. You don't know what it is, but it's there, like " +
                        "a splinter in your mind, driving you mad.",
                        Created = DateTime.ParseExact("2021-11-21 00:00:21", "yyyy-MM-dd hh:mm:ss", null),
                    },
                    new Blog
                    {
                        Author = _db.Users.Where(x => x.UserName == "Lisa").FirstOrDefault(),
                        Title = "Something about Bart...",
                        Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium " +
                        "voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati " +
                        "cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id " +
                        "est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                        Created = DateTime.ParseExact("2021-12-02 07:44:12", "yyyy-MM-dd hh:mm:ss", null),
                    },
                    new Blog
                    {
                        Author = _db.Users.Where(x => x.UserName == "Peter").FirstOrDefault(),
                        Title = "Bla bla bla!",
                        Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium " +
                        "voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati " +
                        "cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id " +
                        "est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                        Created = DateTime.ParseExact("2021-12-10 12:44:12", "yyyy-MM-dd hh:mm:ss", null),
                    },
                    new Blog
                    {
                        Author = _db.Users.Where(x => x.UserName == "Neo").FirstOrDefault(),
                        Title = "Mad mad world!",
                        Body = "What you know you can't explain, but you feel it. You've felt it your entire life, " +
                        "that there's something wrong with the world. You don't know what it is, but it's there, like " +
                        "a splinter in your mind, driving you mad.",
                        Created = DateTime.ParseExact("2021-12-12 12:12:12", "yyyy-MM-dd hh:mm:ss", null),
                    },
                    new Blog
                    {
                        Author = _db.Users.Where(x => x.UserName == "Bruce").FirstOrDefault(),
                        Title = "Something about Robin...",
                        Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium " +
                        "voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati " +
                        "cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id " +
                        "est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                        Created = DateTime.ParseExact("2021-12-14 11:22:00", "yyyy-MM-dd hh:mm:ss", null),
                    },
                    new Blog
                    {
                        Author = _db.Users.Where(x => x.UserName == "Diana").FirstOrDefault(),
                        Title = "Shut up!",
                        Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium " +
                        "voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati " +
                        "cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id " +
                        "est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                        Created = DateTime.ParseExact("2021-12-15 10:01:35", "yyyy-MM-dd hh:mm:ss", null),
                    },
                    new Blog
                    {
                        Author = _db.Users.Where(x => x.UserName == "Neo").FirstOrDefault(),
                        Title = "What is real? Part 2",
                        Body = "What you know you can't explain, but you feel it. You've felt it your entire life, " +
                        "that there's something wrong with the world. You don't know what it is, but it's there, like " +
                        "a splinter in your mind, driving you mad.",
                        Created = DateTime.ParseExact("2021-12-16 00:00:00", "yyyy-MM-dd hh:mm:ss", null),
                    },
                    new Blog
                    {
                        Author = _db.Users.Where(x => x.UserName == "MaryJ").FirstOrDefault(),
                        Title = "Something about Peter...",
                        Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium " +
                        "voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati " +
                        "cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id " +
                        "est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                        Created = DateTime.ParseExact("2021-12-18 08:12:32", "yyyy-MM-dd hh:mm:ss", null),
                    },
                    new Blog
                    {

                        Author = _db.Users.Where(x => x.UserName == "Clark").FirstOrDefault(),
                        Title = "I can fly!",
                        Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium " +
                        "voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati " +
                        "cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id " +
                        "est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                        Created = DateTime.ParseExact("2021-12-20 03:22:33", "yyyy-MM-dd hh:mm:ss", null),
                    }
            };
            foreach (Blog b in Blogs)
            {
                await _db.Blogs.AddAsync(b);
            }
            await _db.SaveChangesAsync();
            }
            // End Seed Blogs
    }
}
