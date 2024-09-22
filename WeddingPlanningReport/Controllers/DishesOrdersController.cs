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
    public class DishesOrdersController : Controller
    {
        private readonly WeddingPlanningContext _context;

        public DishesOrdersController(WeddingPlanningContext context)
        {
            _context = context;
        }

        // GET: DishesOrdersController/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: DishesOrdersController/IndexJson
        public JsonResult IndexJson()
        {
            return Json(_context.DishesOrders);
        }

        //// GET: DishesOrders
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.DishesOrders.ToListAsync());
        //}

        // GET: DishesOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishesOrder = await _context.DishesOrders
                .FirstOrDefaultAsync(m => m.DishesOrderId == id);
            if (dishesOrder == null)
            {
                return NotFound();
            }

            return View(dishesOrder);
        }

        // GET: DishesOrders/Create
        public IActionResult Create()
        {
            var dishesOrder = new DishesOrder(); // 確保創建一個新的模型實例
            return View(dishesOrder);
        }

        // POST: DishesOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishesOrderId,MemberId,DishesOrderName,DishesSupplyDate,DishesTotalPrice")] DishesOrder dishesOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dishesOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dishesOrder);
        }

        // GET: DishesOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishesOrder = await _context.DishesOrders.FindAsync(id);
            if (dishesOrder == null)
            {
                return NotFound();
            }
            return View(dishesOrder);
        }

        // POST: DishesOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishesOrderId,MemberId,DishesOrderName,DishesSupplyDate,DishesTotalPrice")] DishesOrder dishesOrder)
        {
            if (id != dishesOrder.DishesOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dishesOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishesOrderExists(dishesOrder.DishesOrderId))
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
            return View(dishesOrder);
        }

        // GET: DishesOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishesOrder = await _context.DishesOrders
                .FirstOrDefaultAsync(m => m.DishesOrderId == id);
            if (dishesOrder == null)
            {
                return NotFound();
            }

            return View(dishesOrder);
        }

        // POST: DishesOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dishesOrder = await _context.DishesOrders.FindAsync(id);
            if (dishesOrder != null)
            {
                _context.DishesOrders.Remove(dishesOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishesOrderExists(int id)
        {
            return _context.DishesOrders.Any(e => e.DishesOrderId == id);
        }
    }
}
