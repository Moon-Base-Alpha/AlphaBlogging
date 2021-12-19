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
        private readonly bool Add_Users = true;

        //Roles
        //private static readonly string Role1Name = "Admin";
        //private static readonly string Role2Name = "User";


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

        //CreateRole Method
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

        ////Create Admin method
        //private async Task CreateAdminAsync()
        //{
        //    if (await _userManager.FindByEmailAsync("admin@email.com") == null)
        //    {
        //        var user = new ApplicationUser
        //        {
        //            FirstName = "Chi",
        //            LastName = "Larsson",
        //            UserName = "admin",
        //            Email = "admin@email.com",
        //        };
        //        await _userManager.CreateAsync(user, "123456");
        //        await _userManager.AddToRoleAsync(user, "admin");
        //    }
        //}

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

                if (Add_Users)
                {
                    var User = new ApplicationUser
                    {
                        FirstName = "Chi",
                        LastName = "Larsson",
                        UserName = "admin",
                        Email = "admin@email.com",
                    };
                    await _userManager.CreateAsync(User, "123456");
                    await _userManager.AddToRoleAsync(User, "admin");
                }

                //if (!_db.Users.Any(u => u.UserName == "admin"))
                //{
                //    var adminUser = new ApplicationUser
                //    {
                //        FirstName = "Chi",
                //        LastName = "Larsson",
                //        UserName = "admin",
                //        Email = "admin@email.com",
                //    };
                //    await _userManager.CreateAsync(adminUser, "123456");
                //    await _userManager.AddToRoleAsync(adminUser, "admin");
                //}
            }
        }
    }


}
