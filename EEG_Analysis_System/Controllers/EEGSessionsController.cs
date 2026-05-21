using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EEG_Analysis_System.Data;
using EEG_Analysis_System.Models;

namespace EEG_Analysis_System.Controllers
{
    public class EEGSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EEGSessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EEGSessions
        public async Task<IActionResult> Index()
        {
            return View(await _context.EEGSessions.ToListAsync());
        }

        // GET: EEGSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eEGSession = await _context.EEGSessions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eEGSession == null)
            {
                return NotFound();
            }

            return View(eEGSession);
        }

        // GET: EEGSessions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EEGSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,SessionName,CreatedAt,DurationSeconds,Status")] EEGSession eEGSession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eEGSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eEGSession);
        }

        // GET: EEGSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eEGSession = await _context.EEGSessions.FindAsync(id);
            if (eEGSession == null)
            {
                return NotFound();
            }
            return View(eEGSession);
        }

        // POST: EEGSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,SessionName,CreatedAt,DurationSeconds,Status")] EEGSession eEGSession)
        {
            if (id != eEGSession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eEGSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EEGSessionExists(eEGSession.Id))
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
            return View(eEGSession);
        }

        // GET: EEGSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eEGSession = await _context.EEGSessions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eEGSession == null)
            {
                return NotFound();
            }

            return View(eEGSession);
        }

        // POST: EEGSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eEGSession = await _context.EEGSessions.FindAsync(id);
            if (eEGSession != null)
            {
                _context.EEGSessions.Remove(eEGSession);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EEGSessionExists(int id)
        {
            return _context.EEGSessions.Any(e => e.Id == id);
        }
    }
}
