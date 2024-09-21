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
            var returnEvents = _context.Events.Where(eve => eve.CaseId == id).ToList();
            return View(returnEvents);
        }

        public async Task<IActionResult> SchedulesDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var returnSchedules = _context.Schedules.Where(sche => sche.EventId == id).ToList();
            return View(returnSchedules);
        }

        public async Task<IActionResult> StaffsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var returnSchedulesStaff = _context.ScheduledStaffs.Where(scheS => scheS.ScheduleId == id).ToList();
            return View(returnSchedulesStaff);
        }


        // GET: WeddingPlans/Create
        public IActionResult Create()
        {
            var CreateMembers = _context.Members.Select(m => new {
                m.MemberId,
                DisplayName = m.MemberId + " - " + m.MemberName
            }).ToList();

            // 建立下拉選單
            ViewBag.memberId = new SelectList(CreateMembers, "MemberId", "DisplayName");
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
            var allMembers = _context.Members.Select(m => new {
                m.MemberId,
                DisplayName = m.MemberId + " - " + m.MemberName
            }).ToList();

            var defaultMemberId = _context.MemberBudgetItems
                .Where(c => c.BudgetItemId == id)
                .Select(c => c.MemberId)
                .FirstOrDefault();

            ViewBag.memberId = new SelectList(allMembers, "MemberId", "DisplayName", defaultMemberId);
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

            var defaultMemberId = _context.WeddingPlans
                .Where(c => c.CaseId == id)
                .Select(c => c.MemberId)
                .FirstOrDefault();
            if (defaultMemberId != null)
            {
                var memberName = _context.Members
                    .Where(m => m.MemberId == defaultMemberId) // 确保使用正确的字段名
                    .Select(m => m.MemberName) // 假设姓名字段为 Name
                    .FirstOrDefault();

                ViewBag.MemberName = memberName;
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
            return RedirectToAction(nameof(Index)); ;
        }

        private bool WeddingPlanExists(int id)
        {
            return _context.WeddingPlans.Any(e => e.CaseId == id);
        }
    }
}
