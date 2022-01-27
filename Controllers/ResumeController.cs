using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeInformationSystem.Data;
using EmployeeInformationSystem.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInformationSystem.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHost;




        public ResumeController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;

        }
        public IActionResult Index()
        {
            List<Applicant> applicants;
            applicants = _context.Applicants.ToList();
            return View(applicants);
        }

        [HttpGet]

        public IActionResult Create()
        {
            Applicant applicant = new Applicant();
            applicant.Experiences.Add(new Experience() { ExperienceId = 1 });

            return View(applicant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Applicant applicant)
        {
            applicant.Experiences.RemoveAll(n => n.YearsWorked == 0);


            if (ModelState.IsValid)
            {
                string uniqueFileName = GetUploadedFileName(applicant);
                applicant.PhotoUrl = uniqueFileName;

                _context.Add(applicant);
                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction("Index");

        }


        private string GetUploadedFileName(Applicant applicant)
        {
            string uniqueFileName = null;

            if (applicant.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Image/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + applicant.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    applicant.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public IActionResult Details(int Id)
        {
            Applicant applicant = _context.Applicants.Include(e => e.Experiences)
                .Where(a => a.Id == Id).FirstOrDefault();
            return View(applicant);
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Applicant applicant = _context.Applicants.Include(e => e.Experiences)
                .Where(a => a.Id == Id).FirstOrDefault();
            return View(applicant);
        }

        [HttpPost]

        public IActionResult Delete(Applicant applicant)
        {
            _context.Attach(applicant);
            _context.Entry(applicant).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int? Id)
        {

            var applicant = await _context.Applicants
                .Include(q => q.Experiences)
                .Where(d => d.Id == Id).FirstOrDefaultAsync();
            return View(applicant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Applicant applicant)
        {
            if (ModelState.IsValid)
            {

                string uniqueFileName = GetUploadedFileName(applicant);
                applicant.PhotoUrl = uniqueFileName;
                _context.Entry(applicant).State = EntityState.Modified;
                foreach (var ap in applicant.Experiences)
                {
                    _context.Entry(ap).State = EntityState.Modified;
                }
               

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicant);
        }

       

    }
}
