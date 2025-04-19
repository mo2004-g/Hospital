using System.Diagnostics;
using Hos.Date;
using Hos.Models;
using Hos.Reposiories.IReposiories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hos.Controllers
{
    [Area("Pation")]
    
    public class HomeController : Controller
    {
      //  ApplicationDbcontext dbcontext = new ApplicationDbcontext();
     
        private readonly IAppointmentsRepositry appointmentsRepositry;
        private readonly IDoctorRepositry doctorRepositry;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IAppointmentsRepositry appointmentsRepositry, IDoctorRepositry doctorRepositry,UserManager<ApplicationUser> userManager, ILogger<HomeController> logger)
        {
            this.appointmentsRepositry = appointmentsRepositry;
            this.doctorRepositry = doctorRepositry;
            this._userManager = userManager;
            _logger = logger;
        }


        

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BookAppointment()
        {
            var doctor = doctorRepositry.Get();
            return View(doctor.ToList());
            
        }
        [HttpGet]
        public IActionResult CompleteAppointment(int DocId , string userId)
        {
            var user = _userManager.FindByIdAsync(userId);
            var Doctor = doctorRepositry.Getone(e =>e.Id == DocId);
            ViewBag.Doctor = Doctor;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult CompleteAppointment(string Name,DateOnly appointmentDate,TimeOnly appointmentTime, int DocId)
        {
            if (ModelState.IsValid)
            {
                if (appointmentDate < DateOnly.FromDateTime(DateTime.Today))
                {
                    TempData["Error"] = "áÇ íãßä ÍÌÒ ãæÇÚíÏ Ýí ÃíÇã ÓÇÈÞÉ";
                    return RedirectToAction("Index");
                }

                var appointment = appointmentsRepositry.Get(e => e.Day == appointmentDate && e.Time == appointmentTime && e.DoctorId == DocId);
                    if (appointment.Count() > 0)
                    {
                        TempData["Error"] = "åÐÇ ÇáãæÚÏ ãÍÌæÒ ãÓÈÞÇ";
                        return RedirectToAction("Index");
                    }
                
                appointmentsRepositry.Create(new HistoryofDate
                {
                    DoctorId = DocId,
                    Name = Name,
                    Day = appointmentDate,
                    Time = appointmentTime

                });
                appointmentsRepositry.Commit();
                TempData["notification"] = "Êã ÇáÍÌÒ ÈäÌÇÍ";

                return RedirectToAction("Index");
            }
            return View();
        }

        
       public IActionResult ReservationsManagement()
        {
            var his = appointmentsRepositry.Get(includes: [e => e.Doctor] );
            return View(his.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
