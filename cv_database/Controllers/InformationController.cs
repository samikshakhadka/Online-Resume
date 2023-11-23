using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cv_database.Data;
using cv_database.Models;

namespace cv_database.Controllers
{
    public class InformationController : Controller
    {
        private readonly cv_databaseContext _context;

        public InformationController(cv_databaseContext context)
        {
            _context = context;
        }

        // GET: Information
        public async Task<IActionResult> Index()
        {
              return _context.Information != null ? 
                          View(await _context.Information.ToListAsync()) :
                          Problem("Entity set 'cv_databaseContext.Information'  is null.");
        }

        // GET: Information/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Information == null)
            {
                return NotFound();
            }

            var information = await _context.Information
                .FirstOrDefaultAsync(m => m.information_id == id);
            if (information == null)
            {
                return NotFound();
            }

            return View(information);
        }

        // GET: Information/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Information/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("information_id,Name,Password,Email,Summary")] Information information)
        {
            if (ModelState.IsValid)
            {
                _context.Add(information);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(information);
        }

        // GET: Information/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Information == null)
            {
                return NotFound();
            }

            var information = await _context.Information.FindAsync(id);
            if (information == null)
            {
                return NotFound();
            }
            return View(information);
        }

        
    }
}
