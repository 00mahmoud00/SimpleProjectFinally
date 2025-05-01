using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleLibrary.Models;
using SimpleLibrary.Models.AdminViewModels;
using SimpleLibrary.Models.AdminViewModels;
using SimpleLibrary.ViewModels;

namespace SimpleLibrary.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public AdminController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    // public async Task<IActionResult> Login(LoginViewModel model)
    // {
    //     if (!ModelState.IsValid)
    //         return View(model);
    //
    //     var user = await _userManager.FindByEmailAsync(model.Email);
    //     if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
    //     {
    //         var claims = new List<Claim>
    //         {
    //             new Claim(ClaimTypes.Name, user.UserName),
    //             new Claim(ClaimTypes.Email, user.Email)
    //         };
    //
    //         var roles = await _userManager.GetRolesAsync(user);
    //         claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
    //
    //         var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    //
    //         await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
    //
    //         return RedirectToAction("Index", "Home");
    //     }
    //
    //     ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    //     return View(model);
    // }
    [HttpGet]
    public IActionResult Index()
    {
        var users = _userManager.Users.Select(user => new SimpleLibrary.Models.AdminViewModels.AdminUserListViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        }).ToList();

        return View(users);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(AdminUserCreateViewModel model)
    {
        var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        //code to validate that email is exist
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("Index");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        var model = new AdminUserEditViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(AdminUserEditViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByIdAsync(model.Id);
        if (user == null)
            return NotFound();

        user.UserName = model.UserName;
        user.Email = model.Email;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
            return RedirectToAction("Index");

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
            return RedirectToAction("Index");

        return BadRequest("Unable to delete user.");
    }
}