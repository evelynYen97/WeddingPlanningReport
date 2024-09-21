using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingPlanningReport.Models;

namespace WeddingPlanningReport.Controllers
{
    public class ScheduledStaffsController : Controller
    {
        private readonly WeddingPlanningContext _context;

        public ScheduledStaffsController(WeddingPlanningContext context)
        {
            _context = context;
        }

        // GET: ScheduledStaffs
        public async Task<IActionResult> Index()
        {
            return View(await _context.ScheduledStaffs.ToListAsync());
        }

        // GET: ScheduledStaffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduledStaff = await _context.ScheduledStaffs
                .FirstOrDefaultAsync(m => m.PersonnelId == id);
            if (scheduledStaff == null)
            {
                return NotFound();
            }

            return View(scheduledStaff);
        }

        // GET: ScheduledStaffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ScheduledStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonnelId,ScheduleId,PersonnelName,AssistanceContent,IsDelete")] ScheduledStaff scheduledStaff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheduledStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scheduledStaff);
        }

        // GET: ScheduledStaffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduledStaff = await _context.ScheduledStaffs.FindAsync(id);
            if (scheduledStaff == null)
            {
                return NotFound();
            }
            return View(scheduledStaff);
        }

        // POST: ScheduledStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonnelId,ScheduleId,PersonnelName,AssistanceContent,IsDelete")] ScheduledStaff scheduledStaff)
        {
            if (id != scheduledStaff.PersonnelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduledStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduledStaffExists(scheduledStaff.PersonnelId))
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
            return View(scheduledStaff);
        }

        // GET: ScheduledStaffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduledStaff = await _context.ScheduledStaffs
                .FirstOrDefaultAsync(m => m.PersonnelId == id);
            if (scheduledStaff == null)
            {
                return NotFound();
            }

            return View(scheduledStaff);
        }

        // POST: ScheduledStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scheduledStaff = await _context.ScheduledStaffs.FindAsync(id);
            if (scheduledStaff != null)
            {
                _context.ScheduledStaffs.Remove(scheduledStaff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduledStaffExists(int id)
        {
            return _context.ScheduledStaffs.Any(e => e.PersonnelId == id);
        }
    }
}
