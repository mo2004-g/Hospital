using Hos.Models;
using Hos.Reposiories.IReposiories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hos.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorRepositry doctorRepositry;

        public DoctorController(IDoctorRepositry doctorRepositry )
        {
            this.doctorRepositry = doctorRepositry;

        }

        public IActionResult Index()
        {
            var Doctor = doctorRepositry.Get();
            return View(Doctor.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Doctor());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Doctor doctor , IFormFile? file)
        {
            ModelState.Remove("Img");
            ModelState.Remove("History");
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0) 
                {
                    var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filepath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\image",filename);

                    using (var stream = System.IO.File.Create(filepath)) 
                    {
                        file.CopyTo(stream);
                    }
                    doctor.Img = filename;
                }
                doctorRepositry.Create(doctor);
                doctorRepositry.Commit();
                TempData["notification"] = "تم تسجيل البينات بنجاح";
               return RedirectToAction("Index");
            }
            return View(doctor);
        }
        [HttpGet]
        public IActionResult Edit(int DocId) 
        {
            var doc = doctorRepositry.Getone(e =>e.Id == DocId);
            return View(doc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Doctor doctor , IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                var DoctorInDb = doctorRepositry.Getone(e =>e.Id == doctor.Id , tracked:false);
                if (DoctorInDb != null && file != null && file.Length > 0)
                {
                    var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filepth = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\image", filename);
                    using (var stream = System.IO.File.Create(filepth))
                    {
                        file.CopyTo(stream);
                    }

                    var oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\image", DoctorInDb.Img);
                    if (System.IO.File.Exists(oldpath))
                    {
                        System.IO.File.Delete(oldpath);
                    }
                     doctor.Img = filename;
                }
                else
                {
                    doctor.Img = DoctorInDb.Img;
                }
                if (doctor != null) 
                {
                    doctorRepositry.Edit(doctor);
                    doctorRepositry.Commit();
                    TempData["notification"] = "تم تعديل البينات بنجاح";
                    return RedirectToAction("Index");
                }
             
            }
            return View(doctor);
        }

        public IActionResult Delete(int DocId)
        {
            var doctor = doctorRepositry.Getone(e=>e.Id == DocId);

            if (doctor != null)
            {
                // Delete old img from wwwroot
                if (doctor.Img != null)
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", doctor.Img);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                // Delete img name in db
                doctorRepositry.Delete(doctor);
                doctorRepositry.Commit();

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
