using ITI_MVC.Data;
using ITI_MVC.Models;
using ITI_MVC.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITI_MVC.DatabaseInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _db;

        public static bool AdminCreated = false; // 👈 Flag عام

        public DbInitializer(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
            catch { }

            // Create roles
            if (!_roleManager.RoleExistsAsync(StaticDetails.Role_Customer).Result)
                _roleManager.CreateAsync(new Role { Name = StaticDetails.Role_Customer }).Wait();

            if (!_roleManager.RoleExistsAsync(StaticDetails.Role_Admin).Result)
                _roleManager.CreateAsync(new Role { Name = StaticDetails.Role_Admin }).Wait();

            // Create admin
            var email = "ziadelsayed002@gmail.com";
            var user = _userManager.FindByEmailAsync(email).Result;

            if (user == null)
            {
                var createResult = _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "ziadelsayed",
                    Email = email,
                    EmailConfirmed = true,
                    PhoneNumber = "01205465966",
                    Address = "Alexandria, Egypt"
                }, "Admin1#").Result;

                if (createResult.Succeeded)
                {
                    user = _userManager.FindByEmailAsync(email).Result;
                    _userManager.AddToRoleAsync(user, StaticDetails.Role_Admin).Wait();
                    AdminCreated = true; 
                }
            }
        }
    }

}
