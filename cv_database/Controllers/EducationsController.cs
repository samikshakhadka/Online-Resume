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
    public class EducationsController : Controller
    {
        private readonly cv_databaseContext _context;

        public EducationsController(cv_databaseContext context)
        {
            _context = context;
        }

        // GET: Educations
        public async Task<IActionResult> Index()
        {
              return _context.Education != null ? 
                          View(await _context.Education.ToListAsync()) :
                          Problem("Entity set 'cv_databaseContext.Education'  is null.");
        }

        // GET: Educations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Education == null)
            {
                return NotFound();
            }

            var education = await _context.Education
                .FirstOrDefaultAsync(m => m.education_id == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // GET: Educations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Educations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("education_id,name,college_name,start_date,end_date,board_name,degree_name,information_id")] Education education)
        {
            if (ModelState.IsValid)
            {
                _context.Add(education);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(education);
        }

        // GET: Educations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Education == null)
            {
                return NotFound();
            }

            var education = await _context.Education.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            }
            return View(education);
        }

        // POST: Educations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("education_id,name,college_name,start_date,end_date,board_name,degree_name,information_id")] Education education)
        {
            if (id != education.education_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(education);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationExists(education.education_id))
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
            return View(education);
        }

        // GET: Educations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Education == null)
            {
                return NotFound();
            }

            var education = await _context.Education
                .FirstOrDefaultAsync(m => m.education_id == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Education == null)
            {
                return Problem("Entity set 'cv_databaseContext.Education'  is null.");
            }
            var education = await _context.Education.FindAsync(id);
            if (education != null)
            {
                _context.Education.Remove(education);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationExists(int id)
        {
          return (_context.Education?.Any(e => e.education_id == id)).GetValueOrDefault();
        }
    }
}
