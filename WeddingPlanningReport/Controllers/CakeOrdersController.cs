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
    public class CakeOrdersController : Controller
    {
        private readonly WeddingPlanningContext _context;

        public CakeOrdersController(WeddingPlanningContext context)
        {
            _context = context;
        }

        // GET: CakeOrders
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> IndexJson()
        {
            return Json(_context.CakeOrders);
        }


        // GET: CakeOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cakeOrder = await _context.CakeOrders
                .FirstOrDefaultAsync(m => m.CakeOrderId == id);
            if (cakeOrder == null)
            {
                return NotFound();
            }

            return View(cakeOrder);
        }

        // GET: CakeOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CakeOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CakeOrderId,MemberId,CakeOrderTotal,CakeOrderAnnotation,Delivery,Payment,CakeOrderStatus")] CakeOrder cakeOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cakeOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cakeOrder);
        }

        // GET: CakeOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cakeOrder = await _context.CakeOrders.FindAsync(id);
            if (cakeOrder == null)
            {
                return NotFound();
            }
            return View(cakeOrder);
        }

        // POST: CakeOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CakeOrderId,MemberId,CakeOrderTotal,CakeOrderAnnotation,Delivery,Payment,CakeOrderStatus")] CakeOrder cakeOrder)
        {
            if (id != cakeOrder.CakeOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cakeOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CakeOrderExists(cakeOrder.CakeOrderId))
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
            return View(cakeOrder);
        }

        // GET: CakeOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cakeOrder = await _context.CakeOrders
                .FirstOrDefaultAsync(m => m.CakeOrderId == id);
            if (cakeOrder == null)
            {
                return NotFound();
            }

            return View(cakeOrder);
        }

        // POST: CakeOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cakeOrder = await _context.CakeOrders.FindAsync(id);
            if (cakeOrder != null)
            {
                _context.CakeOrders.Remove(cakeOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CakeOrderExists(int id)
        {
            return _context.CakeOrders.Any(e => e.CakeOrderId == id);
        }
    }
}
