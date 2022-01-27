using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EmployeeInformationSystem.Data;
using EmployeeInformationSystem.Mapping;
using EmployeeInformationSystem.Models;
using EmployeeInformationSystem.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInformationSystem.Controllers
{
    public class BranchController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public BranchController(IMapper mapper, ApplicationDbContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Branches.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BranchVM branchVM )
        {
            var branch = _mapper.Map<BranchVM, Branch>(branchVM);
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var singleBranch = await _context.Branches.FindAsync(id);
            var branch = _mapper.Map<Branch, BranchVM>(singleBranch);
            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BranchVM branchVM, int id)
        {
            var singleBranch = await _context.Branches.FindAsync(id);
            
            _mapper.Map(branchVM, singleBranch);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var singleBranch = await _context.Branches.FindAsync(id);
            var branch = _mapper.Map<Branch, BranchVM>(singleBranch);
            return View(branch);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var singleBranch = await _context.Branches.FindAsync(id);
            _context.Remove(singleBranch);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var singleBranch = await _context.Branches.FindAsync(id);
            var branch = _mapper.Map<Branch, BranchVM>(singleBranch);
            return View(branch);
        }
    }
}
