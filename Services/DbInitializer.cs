using AlphaBlogging.Data;
using AlphaBlogging.Models;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace AlphaBlogging.Services
{
    public class DbInitializer : IAsyncInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

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
            if (!_db.Roles.Any())
            {
                await CreateRoleAsync("Superadmin");
                await CreateRoleAsync("Editor");
                await CreateRoleAsync("Contributor");
                await CreateRoleAsync("Subscriber");
                await CreateRoleAsync("Admin");
                await CreateRoleAsync("User");
                await CreateRoleAsync("Author");
                await CreateRoleAsync("SuperUser");


                //Seed users
                if (!_db.Users.Any())
            {

            if (_userManager.FindByEmailAsync("tony@email.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Tony",
                    LastName = "Stark",
                    UserName = "Superadmin",
                    Email = "tony@email.com",
                };
                await _userManager.CreateAsync(user, "123Asd");
                await _userManager.AddToRoleAsync(user, "Superadmin");
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
                await _userManager.CreateAsync(user, "123Asd");
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
                await _userManager.CreateAsync(user, "123Asd");
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
                await _userManager.CreateAsync(user, "123Asd");
                await _userManager.AddToRoleAsync(user, "Author");
            }

            if (_userManager.FindByEmailAsync("jack@email.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Jack",
                    LastName = "Frost",
                    UserName = "Admin",
                    Email = "jack@email.com",
                };
                await _userManager.CreateAsync(user, "123Asd");
                await _userManager.AddToRoleAsync(user, "Admin");
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
                await _userManager.CreateAsync(user, "123Asd");
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
                await _userManager.CreateAsync(user, "123Asd");
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
                await _userManager.CreateAsync(user, "123Asd");
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
                await _userManager.CreateAsync(user, "123Asd");
                await _userManager.AddToRoleAsync(user, "User");
            }
            }
            }
            // End seed users //

            // Seed Blogs
            if (!_db.Blogs.Any())
            {
                var blogs = new List<Blog>()
                {
                    new Blog
                    {
                        Author = _db.Users.Where(x => x.UserName == "Neo").FirstOrDefault(),
                        Title = "What is real? How do you define real?",
                        Body = "What you know you can't explain, but you feel it. You've felt it your entire life, " +
                        "that there's something wrong with the world. You don't know what it is, but it's there, like " +
                        "a splinter in your mind, driving you mad.",
                        Created = DateTime.ParseExact("2021-11-21 00:00:21", "yyyy-MM-dd hh:mm:ss", null),
                        Visible = true,
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
                        Visible = true,
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
                        Visible = true,
                    },
                    new Blog
                    {
                        Author = _db.Users.Where(x => x.UserName == "Neo").FirstOrDefault(),
                        Title = "Mad mad world!",
                        Body = "What you know you can't explain, but you feel it. You've felt it your entire life, " +
                        "that there's something wrong with the world. You don't know what it is, but it's there, like " +
                        "a splinter in your mind, driving you mad.",
                        Created = DateTime.ParseExact("2021-12-12 12:12:12", "yyyy-MM-dd hh:mm:ss", null),
                        Visible = true,
                    },
                    new Blog
                    {
                        Author = _db.Users.Where(x => x.UserName == "Lisa").FirstOrDefault(),
                        Title = "Something about Robin...",
                        Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium " +
                        "voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati " +
                        "cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id " +
                        "est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                        Created = DateTime.ParseExact("2021-12-14 11:22:00", "yyyy-MM-dd hh:mm:ss", null),
                        Visible = true,
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
                        Visible = true,
                    },
                    new Blog
                    {
                        Author = _db.Users.Where(x => x.UserName == "Neo").FirstOrDefault(),
                        Title = "What is real? Part 2",
                        Body = "What you know you can't explain, but you feel it. You've felt it your entire life, " +
                        "that there's something wrong with the world. You don't know what it is, but it's there, like " +
                        "a splinter in your mind, driving you mad.",
                        Created = DateTime.ParseExact("2021-12-16 00:00:00", "yyyy-MM-dd hh:mm:ss", null),
                        Visible = true,
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
                        Visible = true,
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
                        Visible = true,
                    }
           };
                await _db.Blogs.AddRangeAsync(blogs);
                await _db.SaveChangesAsync();
            };
            // End Seed Blogs


            //Seed Post
            if (!_db.Posts.Any())
            {
                var posts = new List<Post>()
            {
                new Post
                {
                    Title = "Post title 1 here...",
                    Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium " +
                    "voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati " +
                    "cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id " +
                    "est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                    Created = DateTime.ParseExact("2021-12-01 11:33:12", "yyyy-MM-dd hh:mm:ss", null),
                    Views = 14,
                    Visible = true,
                    BlogId = 2
                },
                new Post
                {
                    Title = "Post title 2 here...",
                    Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium " +
                    "voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati " +
                    "cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id " +
                    "est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                    Created = DateTime.ParseExact("2021-12-03 07:23:12", "yyyy-MM-dd hh:mm:ss", null),
                    Views = 3,
                    Visible = true,
                    BlogId = 3
                },
                new Post
                {
                    Title = "Post title 3 here...",
                    Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium " +
                    "voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati " +
                    "cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id " +
                    "est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                    Created = DateTime.ParseExact("2021-12-05 03:11:56", "yyyy-MM-dd hh:mm:ss", null),
                    Views = 5,
                    Visible = true,
                    BlogId = 6
                },
                new Post
                {
                    Title = "Post title 4 here...",
                    Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium " +
                    "voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati " +
                    "cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id " +
                    "est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                    Created = DateTime.ParseExact("2021-12-06 09:13:12", "yyyy-MM-dd hh:mm:ss", null),
                    Views = 1,
                    Visible = true,
                    BlogId = 9
                },
                new Post
                {
                    Title = "Post title 5 here...",
                    Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium " +
                    "voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati " +
                    "cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id " +
                    "est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                    Created = DateTime.ParseExact("2021-12-08 01:50:24", "yyyy-MM-dd hh:mm:ss", null),
                    Views = 19,
                    Visible = true,
                    BlogId = 3
                },
                new Post
                {
                    Title = "Post title 6 here...",
                    Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium " +
                    "voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati " +
                    "cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id " +
                    "est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                    Created = DateTime.ParseExact("2021-12-10 05:44:19", "yyyy-MM-dd hh:mm:ss", null),
                    Views = 3,
                    Visible = true,
                    BlogId = 3
                },
                new Post
                {
                    Title = "Post title 7 here...",
                    Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium " +
                    "voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati " +
                    "cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id " +
                    "est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                    Created = DateTime.ParseExact("2021-12-11 11:11:11", "yyyy-MM-dd hh:mm:ss", null),
                    Views = 5,
                    Visible = true,
                    BlogId = 3
                }
            };
                await _db.Posts.AddRangeAsync(posts);
                await _db.SaveChangesAsync();
            };
            // End Seed Posts

            //Seed comments
            if (!_db.Comments.Any())
            {
                var comments = new List<Comment>()
            {
                new Comment
                {
                    Author = _db.Users.Where(x => x.UserName == "Neo").FirstOrDefault(),
                    Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis " +
                    "praesenti mvoluptatum deleniti atque corrupti quos dolores et quas molestias excepturi" +
                    " sint occaecati , similique sunt in culpa qui officia deserunt mollitia animi, facilis est " +
                    "et expedita distinctio. At vero eos et accusmus et iusto odio dignissimos ducimus qui blanditiis " +
                    "praesentiu voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint " +
                    "occaecati, similique sunt in culpa qui officia deserunt mollitia animi. ",
                    Created = DateTime.ParseExact("2021-12-01 12:33:12", "yyyy-MM-dd hh:mm:ss", null),
                    PostId = 1
                },
                new Comment
                {
                    Author = _db.Users.Where(x => x.UserName == "Bruce").FirstOrDefault(),
                    Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis " +
                    "praesenti mvoluptatum deleniti atque corrupti quos dolores et quas molestias excepturi" +
                    " sint occaecati , similique sunt in culpa qui officia deserunt mollitia animi, facilis est " +
                    "et expedita distinctio. At vero eos et accusmus et iusto odio dignissimos ducimus qui blanditiis " +
                    "praesentiu voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint " +
                    "occaecati, similique sunt in culpa qui officia deserunt mollitia animi. ",
                    Created = DateTime.ParseExact("2021-12-03 04:11:56", "yyyy-MM-dd hh:mm:ss", null),
                    PostId = 2
                },
                new Comment
                {
                    Author = _db.Users.Where(x => x.UserName == "Jack").FirstOrDefault(),
                    Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis " +
                    "praesenti mvoluptatum deleniti atque corrupti quos dolores et quas molestias excepturi" +
                    " sint occaecati , similique sunt in culpa qui officia deserunt mollitia animi, facilis est " +
                    "et expedita distinctio. At vero eos et accusmus et iusto odio dignissimos ducimus qui blanditiis " +
                    "praesentiu voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint " +
                    "occaecati, similique sunt in culpa qui officia deserunt mollitia animi. ",
                    Created = DateTime.ParseExact("2021-12-05 11:11:56", "yyyy-MM-dd hh:mm:ss", null),
                    PostId = 3
                },
                new Comment
                {
                    Author = _db.Users.Where(x => x.UserName == "MaryJ").FirstOrDefault(),
                    Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis " +
                    "praesenti mvoluptatum deleniti atque corrupti quos dolores et quas molestias excepturi" +
                    " sint occaecati , similique sunt in culpa qui officia deserunt mollitia animi, facilis est " +
                    "et expedita distinctio. At vero eos et accusmus et iusto odio dignissimos ducimus qui blanditiis " +
                    "praesentiu voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint " +
                    "occaecati, similique sunt in culpa qui officia deserunt mollitia animi. ",
                    Created = DateTime.ParseExact("2021-12-06 10:13:12", "yyyy-MM-dd hh:mm:ss", null),
                    PostId = 4
                },
                new Comment
                {
                    Author = _db.Users.Where(x => x.UserName == "Diana").FirstOrDefault(),
                    Body = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis " +
                    "praesenti mvoluptatum deleniti atque corrupti quos dolores et quas molestias excepturi" +
                    " sint occaecati , similique sunt in culpa qui officia deserunt mollitia animi, facilis est " +
                    "et expedita distinctio. At vero eos et accusmus et iusto odio dignissimos ducimus qui blanditiis " +
                    "praesentiu voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint " +
                    "occaecati, similique sunt in culpa qui officia deserunt mollitia animi. ",
                    Created = DateTime.ParseExact("2021-12-08 02:50:24", "yyyy-MM-dd hh:mm:ss", null),
                    PostId = 5
                },
            };
                await _db.Comments.AddRangeAsync(comments);
                await _db.SaveChangesAsync();
            }
            // End Seed comments

            // Seed tags
            if (!_db.Tags.Any())
            {
                var tags = new List<Tag>()
                {
                    new Tag
                    {
                        HashTag ="#martix"
                    },
                    new Tag
                    {
                        HashTag ="#blablabla"
                    },new Tag
                    {
                        HashTag ="#donuts"
                    },new Tag
                    {
                        HashTag ="#spider"
                    },new Tag
                    {
                        HashTag ="#realworld"
                    },
                    new Tag
                    {
                        HashTag ="#music"
                    },
                    new Tag
                    {
                        HashTag ="#love"
                    },
                };
                await _db.Tags.AddRangeAsync(tags);
                await _db.SaveChangesAsync();
            }
        }
    }
}

