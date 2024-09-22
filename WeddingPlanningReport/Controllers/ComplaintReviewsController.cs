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
    public class ComplaintReviewsController : Controller
    {
        private readonly WeddingPlanningContext _context;

        public ComplaintReviewsController(WeddingPlanningContext context)
        {
            _context = context;
        }

        //// GET: ComplaintReviews
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.ComplaintReviews.ToListAsync());
        //}

        // Get: ComplaintReviews/Index
        public IActionResult Index()
        {
            return View();
        }

        // Get: ComplaintReviews/IndexJson
        public JsonResult IndexJson()
        {
            return Json(_context.ComplaintReviews);
        }


        // GET: ComplaintReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintReview = await _context.ComplaintReviews
                .FirstOrDefaultAsync(m => m.ComplaintRecordId == id);
            if (complaintReview == null)
            {
                return NotFound();
            }

            return PartialView("_Details", complaintReview); // 返回部分視圖

        }

        // GET: ComplaintReviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComplaintReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComplaintRecordId,ReporterMemberId,RespondentMemberId,SharedRecordId,ReportTime,ReportDetail,ReviewStatus,ReviewTime,ReviewerId,ReviewerName,ReviewNotes,ReviewResultDescription")] ComplaintReview complaintReview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaintReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaintReview);
        }

        // GET: ComplaintReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintReview = await _context.ComplaintReviews.FindAsync(id);
            if (complaintReview == null)
            {
                return NotFound();
            }
           
            return PartialView("_Edit", complaintReview); // 返回部分視圖

        }

        // POST: ComplaintReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComplaintRecordId,ReporterMemberId,RespondentMemberId,SharedRecordId,ReportTime,ReportDetail,ReviewStatus,ReviewTime,ReviewerId,ReviewerName,ReviewNotes,ReviewResultDescription")] ComplaintReview complaintReview)
        {
            if (id != complaintReview.ComplaintRecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaintReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintReviewExists(complaintReview.ComplaintRecordId))
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
            return View(complaintReview);
        }

        // GET: ComplaintReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintReview = await _context.ComplaintReviews
                .FirstOrDefaultAsync(m => m.ComplaintRecordId == id);
            if (complaintReview == null)
            {
                return NotFound();
            }

            return View(complaintReview);
        }

        // POST: ComplaintReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaintReview = await _context.ComplaintReviews.FindAsync(id);
            if (complaintReview != null)
            {
                _context.ComplaintReviews.Remove(complaintReview);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintReviewExists(int id)
        {
            return _context.ComplaintReviews.Any(e => e.ComplaintRecordId == id);
        }
    }
}
