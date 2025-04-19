using Hos.Models;
using Hos.Reposiories.IReposiories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace Hos.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IAppliationUserReposity _userReposity;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IAppliationUserReposity userReposity , UserManager<ApplicationUser> userManager)
        {
            this._userReposity = userReposity;
            this._userManager = userManager;
        }
        public IActionResult Index(string? query , int page=1)
        {
            var users = _userReposity.Get();
            if(query != null)
            {
                users = users.Where(u => u.UserName.Contains(query) || u.Email.Contains(query));
            }
            var totalpage = Math.Ceiling((decimal)(users.ToList().Count() / 5));
            if(totalpage < page - 1)
            {
                return RedirectToAction("error", "User",new {area="Admin"});
            }
            else
            {
                users = users.Skip((page - 1) * 5).Take(5);
            }
           


            ViewBag.TotalPage = totalpage;

            return View(users.ToList());
        }
        [HttpGet]
        public IActionResult Block(string userId)
        {
            var user = _userReposity.Getone(e => e.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
            
        }

        [HttpPost]
        public async Task<IActionResult> Block(string userId ,int year , int month ,int day, int hour) 
        {
            
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                 return NotFound();
            }
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.UtcNow)
            {
                TempData["Error"] = "هذا المستخدم محظور بالفعل حتى " + user.LockoutEnd.Value.ToLocalTime().ToString("f");
                return RedirectToAction("Index");
            }

            var blockDuration = DateTime.UtcNow
                .AddYears(year)
                .AddMonths(month)
                .AddDays(day)
                .AddHours(hour);
                

            user.LockoutEnd = blockDuration;

            await _userManager.UpdateAsync(user);
            TempData["notification"] = "تم حظر المستخدم بنجاح حتى " + blockDuration.ToLocalTime().ToString("f");
            return RedirectToAction("Index");

        }
        
        public async Task<IActionResult> Unblock(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.LockoutEnd = null; // معناها مش متقفل

            await _userManager.UpdateAsync(user);

            TempData["notification"] = "تم فك الحظر عن المستخدم بنجاح.";
            return RedirectToAction("Index");
        }

        public IActionResult error()
        {
            return View();
        }
    }
}
