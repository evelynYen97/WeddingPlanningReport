using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingPlanningReport.Models;
using WeddingPlanningReport.Models.Metadata;

namespace WeddingPlanningReport.Controllers
{
    public class WeddingPlansController : Controller
    {
        private readonly WeddingPlanningContext _context;

        public WeddingPlansController(WeddingPlanningContext context)
        {
            _context = context;
        }

        // GET: WeddingPlans
        public async Task<IActionResult> Index()
        {
            return View( _context.WeddingPlans);
        }


        // GET: WeddingPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events
                .FirstOrDefaultAsync(m => m.CaseId == id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // GET: WeddingPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeddingPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CaseId,MemberId,WeddingName,Introduction,WeddingTime,WeddingLocation")] WeddingPlan weddingPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weddingPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weddingPlan);
        }

        // GET: WeddingPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingPlan = await _context.WeddingPlans.FindAsync(id);
            if (weddingPlan == null)
            {
                return NotFound();
            }
            return View(weddingPlan);
        }

        // POST: WeddingPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseId,MemberId,WeddingName,Introduction,WeddingTime,WeddingLocation")] WeddingPlan weddingPlan)
        {
            if (id != weddingPlan.CaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weddingPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeddingPlanExists(weddingPlan.CaseId))
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
            return View(weddingPlan);
        }

        // GET: WeddingPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingPlan = await _context.WeddingPlans
                .FirstOrDefaultAsync(m => m.CaseId == id);
            if (weddingPlan == null)
            {
                return NotFound();
            }

            return View(weddingPlan);
        }

        // POST: WeddingPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weddingPlan = await _context.WeddingPlans.FindAsync(id);
            if (weddingPlan != null)
            {
                _context.WeddingPlans.Remove(weddingPlan);
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true});
        }

        private bool WeddingPlanExists(int id)
        {
            return _context.WeddingPlans.Any(e => e.CaseId == id);
        }
    }
}
