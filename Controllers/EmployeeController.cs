using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeInformationSystem.Data;
using EmployeeInformationSystem.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace EmployeeInformationSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        public EmployeeController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }


        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Employees.Include(e => e.Branch).Include(e => e.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Branch)
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            ViewData["BranchName"] = new SelectList(_context.Branches, "BranchID", "BranchName");
            ViewData["DepartmentName"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = GetUploadedFileName(employee);
                employee.PhotoUrl = uniqueFileName;
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchName"] = new SelectList(_context.Branches, "BranchID", "BranchName", employee.BranchID);
            ViewData["DepartmentName"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", employee.DepartmentID);
            return View(employee);
        }

        private string GetUploadedFileName(Employee employee)
        {
            string uniqueFileName = null;

            if (employee.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Image/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + employee.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    employee.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        

        public async Task<IActionResult> Edit(int? id)
        {

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["BranchName"] = new SelectList(_context.Branches, "BranchID", "BranchName", employee.BranchID);
            ViewData["DepartmentName"] = new SelectList(_context.Departments, "BranchID", "DepartmentName", employee.DepartmentID);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee )
        {
            if (ModelState.IsValid)
            {

                string uniqueFileName = GetUploadedFileName(employee);
                employee.PhotoUrl = uniqueFileName;
                _context.Entry(employee).State = EntityState.Modified;


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchName"] = new SelectList(_context.Branches, "BranchID", "BranchName", employee.BranchID);
            ViewData["DepartmentName"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", employee.DepartmentID);
            return View(employee);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Branch)
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }
    }
}
