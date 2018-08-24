using System.Linq;
using Cinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data
{
    public class ApplicationDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void Seed()
        {
            // TWORZENIE BAZY
              _context.Database.Migrate();


            if (!_context.Roles.Any())
            {
                var roleNames = new[]
                {
                    Roles.Roles.Administrator,
                    Roles.Roles.Employee,

                };

                foreach (var roleName in roleNames)
                {
                    var role = new IdentityRole(roleName) { NormalizedName = roleName.ToUpper() };
                    _context.Roles.Add(role);
                }
            }


            if (!_context.ApplicationUsers.Any())
            {
                const string userName = "admin@admin.com";                                     // ADMIN
                const string userPass = "p@sw1ooorD";

                var user = new ApplicationUser { UserName = userName, Email = userName };
                _userManager.CreateAsync(user, userPass).Wait();
                _userManager.AddToRoleAsync(user, Roles.Roles.Administrator).Wait();

                const string userName2 = "employee@employee.com";
                const string userPass2 = "p@sw1ooorD";

                var user2 = new ApplicationUser { UserName = userName2, Email = userName2 };
                _userManager.CreateAsync(user2, userPass2).Wait();
                _userManager.AddToRoleAsync(user2, Roles.Roles.Employee).Wait();

            }

            _context.SaveChanges();
        }

    }

}
