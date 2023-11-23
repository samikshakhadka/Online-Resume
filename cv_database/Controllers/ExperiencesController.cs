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
    public class ExperiencesController : Controller
    {
        private readonly cv_databaseContext _context;

        public ExperiencesController(cv_databaseContext context)
        {
            _context = context;
        }

        // GET: Experiences
        public async Task<IActionResult> Index()
        {
              return _context.Experience != null ? 
                          View(await _context.Experience.ToListAsync()) :
                          Problem("Entity set 'cv_databaseContext.Experience'  is null.");
        }

        // GET: Experiences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Experience == null)
            {
                return NotFound();
            }

            var experience = await _context.Experience
                .FirstOrDefaultAsync(m => m.experience_id == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // GET: Experiences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Experiences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("experience_id,institution_name,position,star_tdate,end_date,address,information_id")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(experience);
        }

        // GET: Experiences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Experience == null)
            {
                return NotFound();
            }

            var experience = await _context.Experience.FindAsync(id);
            if (experience == null)
            {
                return NotFound();
            }
            return View(experience);
        }

        // POST: Experiences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("experience_id,institution_name,position,star_tdate,end_date,address,information_id")] Experience experience)
        {
            if (id != experience.experience_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(experience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienceExists(experience.experience_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(experience);
        }

        // GET: Experiences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Experience == null)
            {
                return NotFound();
            }

            var experience = await _context.Experience
                .FirstOrDefaultAsync(m => m.experience_id == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // POST: Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Experience == null)
            {
                return Problem("Entity set 'cv_databaseContext.Experience'  is null.");
            }
            var experience = await _context.Experience.FindAsync(id);
            if (experience != null)
            {
                _context.Experience.Remove(experience);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperienceExists(int id)
        {
          return (_context.Experience?.Any(e => e.experience_id == id)).GetValueOrDefault();
        }
    }
}
