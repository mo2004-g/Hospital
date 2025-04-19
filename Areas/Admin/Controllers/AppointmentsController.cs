using Hos.Models;
using Hos.Reposiories.IReposiories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Hos.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AppointmentsController : Controller
    {
       
        private readonly IDoctorRepositry doctorRepositry;
        private readonly IAppointmentsRepositry appointmentRepositry;

        public AppointmentsController(IAppointmentsRepositry appointmentRepositry, IDoctorRepositry doctorRepositry)
        {
            this.appointmentRepositry = appointmentRepositry;
            this.doctorRepositry = doctorRepositry;
        }

        public IActionResult Index()
        {
            var appointments = appointmentRepositry.Get(includes: [e=>e.Doctor]);
            return View(appointments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var doctors = doctorRepositry.Get();
            ViewData["Doctor"] = doctors.ToList();
            return View(new HistoryofDate());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HistoryofDate historyofDate)
        {
            
            if (ModelState.IsValid)
            {
                appointmentRepositry.Create(historyofDate);
                appointmentRepositry.Commit();
                TempData["notification"] = "تم الحجز بنجاح";
                return RedirectToAction("Index");
            }
            var doctors = doctorRepositry.Get();
            ViewData["Doctor"] = doctors.ToList();
            return View(historyofDate);
        }
        [HttpGet]
        public IActionResult Edit(int pationid ) 
        {
            var a=appointmentRepositry.Getone(e=>e.Id==pationid );
            var doctors = doctorRepositry.Get();
            ViewData["Doctor"] = doctors.ToList();
            if (a != null)
            {
                return View(a);
            }
            return View();
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HistoryofDate historyofDate)
        {
            if (ModelState.IsValid)
            {
                appointmentRepositry.Edit(historyofDate);
                appointmentRepositry.Commit();
                TempData["notification"] = "تم تعديل الحجز بنجاح";
                return RedirectToAction("Index");
            }
            
            return View(historyofDate);
        }
        public IActionResult Delete(int PationId) 
        {
            var pa = appointmentRepositry.Getone(e=>e.Id == PationId);
            if (pa != null)
            {
                appointmentRepositry.Delete(pa);
                appointmentRepositry.Commit();
              
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult DeleteAll()
        {
            var pa = appointmentRepositry.Get();
            if (pa != null)
            {
                appointmentRepositry.Delete(pa.ToList());
                appointmentRepositry.Commit();
                
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
