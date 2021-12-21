using AlphaBlogging.Data;
using AlphaBlogging.Models;

using Extensions.Hosting.AsyncInitialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using System.Threading.Tasks;
namespace AlphaBlogging.Services
{
    public class DbInitializer : IAsyncInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly bool InitializeDb = true;


        //What to add
        private readonly bool Add_Roles = true;

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


        #region CreateRole SeedUsers Method
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

                if (Add_Roles)
                {
                    await CreateRoleAsync("Admin");
                    await CreateRoleAsync("User");
                    //await CreateRoleAsync("SuperUser");
                }
            
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
                    UserName = "Neo@Matrix",
                    Email = "neo@email.com",
                };
                await _userManager.CreateAsync(user, "123456");
                await _userManager.AddToRoleAsync(user, "User");
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
                await _userManager.AddToRoleAsync(user, "User");
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
                await _userManager.AddToRoleAsync(user, "User");
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
                await _userManager.AddToRoleAsync(user, "User");
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
                await _userManager.AddToRoleAsync(user, "User");
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
                await _userManager.AddToRoleAsync(user, "User");
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
                await _userManager.AddToRoleAsync(user, "User");
            }

            if (_userManager.FindByEmailAsync("bruce@email.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Bruce",
                    LastName = "Wayne",
                    UserName = "bruce",
                    Email = "bruce@email.com",
                };
                await _userManager.CreateAsync(user, "123456");
                await _userManager.AddToRoleAsync(user, "User");
            }
            }
        }
        #endregion


        

    }
}