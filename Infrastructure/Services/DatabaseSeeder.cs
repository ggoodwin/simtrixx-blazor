using Application.Interfaces.Services;
using Common.Constants.Permission;
using Common.Constants.Role;
using Common.Constants.User;
using Domain.Entities.Identity;
using Infrastructure.Contexts;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ILogger<DatabaseSeeder> _logger;
        private readonly DataContext _db;
        private readonly UserManager<SimtrixxUser> _userManager;
        private readonly RoleManager<SimtrixxRole> _roleManager;

        public DatabaseSeeder(
            UserManager<SimtrixxUser> userManager,
            RoleManager<SimtrixxRole> roleManager,
            DataContext db,
            ILogger<DatabaseSeeder> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _logger = logger;
        }

        public void Initialize()
        {
            AddAdministrator();
            AddBasicUser();
            _db.SaveChanges();
        }

        private void AddAdministrator()
        {
            Task.Run(async () =>
            {
                //Check if Role Exists
                var adminRole = new SimtrixxRole(RoleConstants.AdministratorRole, "Administrator role with full permissions");
                var adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                if (adminRoleInDb == null)
                {
                    await _roleManager.CreateAsync(adminRole);
                    adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                    _logger.LogInformation("Seeded Administrator Role.");
                }
                //Check if User Exists
                var superUser = new SimtrixxUser
                {
                    FirstName = "Greg",
                    LastName = "Goodwin",
                    Email = "greggoodwin@gmail.com",
                    UserName = "ggoodwin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };
                var superUserInDb = await _userManager.FindByEmailAsync(superUser.Email);
                if (superUserInDb == null)
                {
                    await _userManager.CreateAsync(superUser, UserConstants.DefaultPassword);
                    var result = await _userManager.AddToRoleAsync(superUser, RoleConstants.AdministratorRole);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Seeded Default SuperAdmin User.");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _logger.LogError(error.Description);
                        }
                    }
                }
                foreach (var permission in Permissions.GetRegisteredPermissions())
                {
                    await _roleManager.AddPermissionClaim(adminRoleInDb, permission);
                }

                //Add second user
                var superUser2 = new SimtrixxUser
                {
                    FirstName = "Brad",
                    LastName = "McCormick",
                    Email = "brad.bradlmccormick@gmail.com",
                    UserName = "bmccormick",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };
                var superUserInDb2 = await _userManager.FindByEmailAsync(superUser2.Email);
                if (superUserInDb2 == null)
                {
                    await _userManager.CreateAsync(superUser2, UserConstants.DefaultPassword);
                    var result = await _userManager.AddToRoleAsync(superUser2, RoleConstants.AdministratorRole);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Seeded Default SuperAdmin User.");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _logger.LogError(error.Description);
                        }
                    }
                }
                foreach (var permission in Permissions.GetRegisteredPermissions())
                {
                    await _roleManager.AddPermissionClaim(adminRoleInDb, permission);
                }
            }).GetAwaiter().GetResult();
        }

        private void AddBasicUser()
        {
            Task.Run(async () =>
            {
                //Check if Role Exists
                var basicRole = new SimtrixxRole(RoleConstants.BasicRole, "Basic role with default permissions");
                var basicRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.BasicRole);
                if (basicRoleInDb == null)
                {
                    await _roleManager.CreateAsync(basicRole);
                    _logger.LogInformation("Seeded Basic Role.");
                }
                //Check if User Exists
                var basicUser = new SimtrixxUser
                {
                    FirstName = "Greg",
                    LastName = "Goodwin",
                    Email = "greg@simtrixx.com",
                    UserName = "greggoodwin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };
                var basicUserInDb = await _userManager.FindByEmailAsync(basicUser.Email);
                if (basicUserInDb == null)
                {
                    await _userManager.CreateAsync(basicUser, UserConstants.DefaultPassword);
                    await _userManager.AddToRoleAsync(basicUser, RoleConstants.BasicRole);
                    _logger.LogInformation("Seeded User with Basic Role.");
                }
                //Second User
                var basicUser2 = new SimtrixxUser
                {
                    FirstName = "Al",
                    LastName = "McCormick",
                    Email = "samccormick853@gmail.com",
                    UserName = "amccormick",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };
                var basicUserInDb2 = await _userManager.FindByEmailAsync(basicUser2.Email);
                if (basicUserInDb2 == null)
                {
                    await _userManager.CreateAsync(basicUser2, UserConstants.DefaultPassword);
                    await _userManager.AddToRoleAsync(basicUser2, RoleConstants.BasicRole);
                    _logger.LogInformation("Seeded User with Basic Role.");
                }
            }).GetAwaiter().GetResult();
        }
    }
}
