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
    public class BudgetChartsController : Controller
    {
        private readonly WeddingPlanningContext _context;

        public BudgetChartsController(WeddingPlanningContext context)
        {
            _context = context;
        }

        // GET: BudgetCharts
        public async Task<IActionResult> Index()
        {
            return View(await _context.BudgetCharts.ToListAsync());
        }

        // GET: BudgetCharts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetChart = await _context.BudgetCharts
                .FirstOrDefaultAsync(m => m.BudgetChartId == id);
            if (budgetChart == null)
            {
                return NotFound();
            }

            return View(budgetChart);
        }

        // GET: BudgetCharts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BudgetCharts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BudgetChartId,MemberId,ChartType,ChartName,ChartUnit,RentalDetailId,DishesOrderDetailId,CakeDetailId,VenueId")] BudgetChart budgetChart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(budgetChart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(budgetChart);
        }

        // GET: BudgetCharts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetChart = await _context.BudgetCharts.FindAsync(id);
            if (budgetChart == null)
            {
                return NotFound();
            }
            return View(budgetChart);
        }

        // POST: BudgetCharts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BudgetChartId,MemberId,ChartType,ChartName,ChartUnit,RentalDetailId,DishesOrderDetailId,CakeDetailId,VenueId")] BudgetChart budgetChart)
        {
            if (id != budgetChart.BudgetChartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(budgetChart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetChartExists(budgetChart.BudgetChartId))
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
            return View(budgetChart);
        }

        // GET: BudgetCharts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetChart = await _context.BudgetCharts
                .FirstOrDefaultAsync(m => m.BudgetChartId == id);
            if (budgetChart == null)
            {
                return NotFound();
            }

            return View(budgetChart);
        }

        // POST: BudgetCharts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budgetChart = await _context.BudgetCharts.FindAsync(id);
            if (budgetChart != null)
            {
                _context.BudgetCharts.Remove(budgetChart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetChartExists(int id)
        {
            return _context.BudgetCharts.Any(e => e.BudgetChartId == id);
        }
    }
}
