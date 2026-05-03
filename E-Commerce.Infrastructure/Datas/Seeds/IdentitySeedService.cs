using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class IdentitySeedService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public IdentitySeedService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        const string roleName = "OwnerAdmin";
        const string email = "rodri@hotmail.com";
        const string password = "Admin1234";

        // 1. ROLE
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            var roleResult = await _roleManager.CreateAsync(
                new IdentityRole<Guid>(roleName));

            if (!roleResult.Succeeded)
                throw new Exception(string.Join(" | ", roleResult.Errors.Select(x => x.Description)));
        }

        // 2. USER
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = email,
                Email = email,
                Nombre = "Owner",
                Apellido = "Admin",
                DNI = "00000000",
                UrlImagen = "",
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception(string.Join(" | ", result.Errors.Select(x => x.Description)));
        }

        // 3. ROLE ASSIGN
        if (!await _userManager.IsInRoleAsync(user, roleName))
        {
            var addRoleResult = await _userManager.AddToRoleAsync(user, roleName);

            if (!addRoleResult.Succeeded)
                throw new Exception(string.Join(" | ", addRoleResult.Errors.Select(x => x.Description)));
        }
    }
}