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
    public class contactsController : Controller
    {
        private readonly cv_databaseContext _context;

        public contactsController(cv_databaseContext context)
        {
            _context = context;
        }

        // GET: contacts
        public async Task<IActionResult> Index()
        {
              return _context.contact != null ? 
                          View(await _context.contact.ToListAsync()) :
                          Problem("Entity set 'cv_databaseContext.contact'  is null.");
        }

        // GET: contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.contact == null)
            {
                return NotFound();
            }

            var contact = await _context.contact
                .FirstOrDefaultAsync(m => m.contact_id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("contact_id,contact_name,contact_link,contact_href,information_id")] contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.contact == null)
            {
                return NotFound();
            }

            var contact = await _context.contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("contact_id,contact_name,contact_link,contact_href,information_id")] contact contact)
        {
            if (id != contact.contact_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!contactExists(contact.contact_id))
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
            return View(contact);
        }

        // GET: contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.contact == null)
            {
                return NotFound();
            }

            var contact = await _context.contact
                .FirstOrDefaultAsync(m => m.contact_id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.contact == null)
            {
                return Problem("Entity set 'cv_databaseContext.contact'  is null.");
            }
            var contact = await _context.contact.FindAsync(id);
            if (contact != null)
            {
                _context.contact.Remove(contact);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool contactExists(int id)
        {
          return (_context.contact?.Any(e => e.contact_id == id)).GetValueOrDefault();
        }
    }
}
