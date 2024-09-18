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
    public class SharingWeddingPlansController : Controller
    {
        private readonly WeddingPlanningContext _context;

        public SharingWeddingPlansController(WeddingPlanningContext context)
        {
            _context = context;
        }

        // GET: SharingWeddingPlans
        public async Task<IActionResult> Index()
        {
            return View(await _context.SharingWeddingPlans.ToListAsync());
        }

        // GET: SharingWeddingPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingWeddingPlan = await _context.SharingWeddingPlans
                .FirstOrDefaultAsync(m => m.SharedRecordId == id);
            if (sharingWeddingPlan == null)
            {
                return NotFound();
            }

            return View(sharingWeddingPlan);
        }

        // GET: SharingWeddingPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SharingWeddingPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SharedRecordId,CaseId,SharedTime,SharedName,SharedStatus")] SharingWeddingPlan sharingWeddingPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sharingWeddingPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sharingWeddingPlan);
        }

        // GET: SharingWeddingPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingWeddingPlan = await _context.SharingWeddingPlans.FindAsync(id);
            if (sharingWeddingPlan == null)
            {
                return NotFound();
            }
            return View(sharingWeddingPlan);
        }

        // POST: SharingWeddingPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SharedRecordId,CaseId,SharedTime,SharedName,SharedStatus")] SharingWeddingPlan sharingWeddingPlan)
        {
            if (id != sharingWeddingPlan.SharedRecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sharingWeddingPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SharingWeddingPlanExists(sharingWeddingPlan.SharedRecordId))
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
            return View(sharingWeddingPlan);
        }

        // GET: SharingWeddingPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharingWeddingPlan = await _context.SharingWeddingPlans
                .FirstOrDefaultAsync(m => m.SharedRecordId == id);
            if (sharingWeddingPlan == null)
            {
                return NotFound();
            }

            return View(sharingWeddingPlan);
        }

        // POST: SharingWeddingPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sharingWeddingPlan = await _context.SharingWeddingPlans.FindAsync(id);
            if (sharingWeddingPlan != null)
            {
                _context.SharingWeddingPlans.Remove(sharingWeddingPlan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SharingWeddingPlanExists(int id)
        {
            return _context.SharingWeddingPlans.Any(e => e.SharedRecordId == id);
        }
    }
}
