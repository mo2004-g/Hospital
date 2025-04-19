using Hos.Models;
using Hos.Models.Viewmodel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Hos.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
       
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (_roleManager.Roles.IsNullOrEmpty())
            {
                await _roleManager.CreateAsync(new IdentityRole("Pation"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerVM.Username,
                    Email = registerVM.email,
                    

                };
                var result = await _userManager.CreateAsync(user, registerVM.password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user,  false);
                    await _userManager.AddToRoleAsync(user,"Pation");
                    return RedirectToAction("Index", "Home", new { area = "Pation" });
                }
                else 
                {
                    ModelState.AddModelError("Password", "Password is not corect");
                }
            }
            return View(registerVM);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                
                var appUser = await _userManager.FindByEmailAsync(loginVM.Email);
                appUser.LockedAt = DateTime.UtcNow;
                if (appUser != null)
                {
                    if (await _userManager.IsLockedOutAsync(appUser))
                    {
                        await _userManager.GetLockoutEndDateAsync(appUser);
                        string lockTime = appUser.LockedAt?.ToLocalTime().ToString("dd MMMM yyyy - hh:mm tt") ?? "وقت غير معروف";
                        var lockoutEnd = await _userManager.GetLockoutEndDateAsync(appUser);
                        string unlockTime = lockoutEnd?.ToLocalTime().ToString("dd MMMM yyyy - hh:mm tt") ?? "غير محدد";

                        ModelState.AddModelError("Email", $"تم حظر حسابك منذ {lockTime}، وسوف ينتهي الحظر في {unlockTime}");
                        return View(loginVM);
                    }

                    var result = await _userManager.CheckPasswordAsync(appUser, loginVM.Password);
                    if (result)
                    {
                        await _signInManager.SignInAsync(appUser, loginVM.RememberMe);
                        return RedirectToAction("Index", "Home", new { area = "Pation" });
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Email is not corect");
                        ModelState.AddModelError("Password", "Password is not corect");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Email is not corect");
                    ModelState.AddModelError("Password", "Password is not corect");
                }
                
            }
            return View(loginVM);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }
    }
}
