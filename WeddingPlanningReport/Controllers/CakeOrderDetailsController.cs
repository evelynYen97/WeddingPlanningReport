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
    public class CakeOrderDetailsController : Controller
    {
        private readonly WeddingPlanningContext _context;

        public CakeOrderDetailsController(WeddingPlanningContext context)
        {
            _context = context;
        }

        // GET: CakeOrderDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.CakeOrderDetails.ToListAsync());
        }

        // GET: CakeOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cakeOrderDetail = await _context.CakeOrderDetails
                .FirstOrDefaultAsync(m => m.CakeOrderDetailId == id);
            if (cakeOrderDetail == null)
            {
                return NotFound();
            }

            return View(cakeOrderDetail);
        }

        // GET: CakeOrderDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CakeOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CakeOrderDetailId,CakeId,CakeOrderId,CakePrice,CakeAmount,CakeSubtotal")] CakeOrderDetail cakeOrderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cakeOrderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cakeOrderDetail);
        }

        // GET: CakeOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cakeOrderDetail = await _context.CakeOrderDetails.FindAsync(id);
            if (cakeOrderDetail == null)
            {
                return NotFound();
            }
            return View(cakeOrderDetail);
        }

        // POST: CakeOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CakeOrderDetailId,CakeId,CakeOrderId,CakePrice,CakeAmount,CakeSubtotal")] CakeOrderDetail cakeOrderDetail)
        {
            if (id != cakeOrderDetail.CakeOrderDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cakeOrderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CakeOrderDetailExists(cakeOrderDetail.CakeOrderDetailId))
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
            return View(cakeOrderDetail);
        }

        // GET: CakeOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cakeOrderDetail = await _context.CakeOrderDetails
                .FirstOrDefaultAsync(m => m.CakeOrderDetailId == id);
            if (cakeOrderDetail == null)
            {
                return NotFound();
            }

            return View(cakeOrderDetail);
        }

        // POST: CakeOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cakeOrderDetail = await _context.CakeOrderDetails.FindAsync(id);
            if (cakeOrderDetail != null)
            {
                _context.CakeOrderDetails.Remove(cakeOrderDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CakeOrderDetailExists(int id)
        {
            return _context.CakeOrderDetails.Any(e => e.CakeOrderDetailId == id);
        }
    }
}
