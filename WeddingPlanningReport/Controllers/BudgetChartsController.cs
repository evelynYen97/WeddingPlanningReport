using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingPlanningReport.Models;
using WeddingPlanningReport.Models.ViewModel;

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

        [HttpPost]
        public IActionResult GetOrderDetails(int chartID,string imageName)
        {
            List<int>? orderId = null;
            int memberID = _context.BudgetCharts.Where(cID => cID.BudgetChartId == chartID).Select(cID => cID.MemberId).FirstOrDefault();

            if (imageName == "總預算分配")
            {
                int? totalvenue=_context.Venues.Where(v=>v.MemberId==memberID).Select(v=>v.VenueRentalPrice).Sum();
                int? totalcake=_context.CakeOrders.Where(c => c.MemberId == memberID).Select(c=>c.CakeOrderTotal).Sum();
                int? totalcar= _context.CarRentals.Where(c => c.MemberId == memberID).Select(c => c.RentalTotal).Sum();
                int? totaldishes= _context.DishesOrders.Where(c => c.MemberId == memberID).Select(c => c.DishesTotalPrice).Sum();
                int? totalothers=_context.MemberBudgetItems.Where(c => c.MemberId == memberID).Select(c => c.BudgetItemSubtotal).Sum();
                int? general = totalvenue + totalcake + totalcar + totaldishes + totalothers;
                //var viewModel = new OrdersDetailsViewModel
                //{
                //    VenueOrdersTotal = totalvenue,
                //    CakeOrdersTotal = totalcake,
                //    DishesOrdersTotal=totaldishes,
                //    CarOrdersTotal=totalcar,
                //    OtherOrdersTotal=totalothers,
                //    OrdersTotal=general,
                //};
                return View();
            }
            else if (imageName == "婚禮場地細項")
            {
                List<int> orderID =_context.Venues.Where(v=>v.MemberId==memberID).Select(oid=>oid.VenueId).ToList();

                orderId = orderID;
            }
            else if (imageName == "喜餅訂購細項")
            {
                List<int> orderID =_context.CakeOrders.Where(v => v.MemberId == memberID).Select(oid => oid.CakeOrderId).ToList();
                List<int> orderDID =_context.CakeOrderDetails.Where(cod=> orderID.Contains(cod.CakeId)).Select(cod=>cod.CakeOrderDetailId).ToList();
                orderId = orderDID;
            }
            else if (imageName == "禮車預訂細項")
            {
                List<int> orderID = _context.CarRentals.Where(v => v.MemberId == memberID).Select(oid => oid.RentalId).ToList();
                List<int> orderDID =_context.CarRentalDetails.Where(cod => orderID.Contains(cod.RentalId)).Select(cod => cod.RentalDetailId).ToList();
                orderId = orderDID;
            }
            else if (imageName == "菜品訂購細項")
            {
                List<int> orderID = _context.DishesOrders.Where(v => v.MemberId == memberID).Select(oid => oid.DishesOrderId).ToList();
                List<int> orderDID = _context.DishesOrderDetails.Where(cod =>orderID.Contains(cod.DishesId)).Select(cod => cod.DishesOrderDetailId).ToList();
                orderId=orderDID;
            }
            else if (imageName == "其他細項")
            {
                List<int> orderID =_context.MemberBudgetItems.Where(v => v.MemberId== memberID).Select(oid => oid.BudgetItemId).ToList();
                orderId = orderID;
            }

            return View();
        }

        // 新的Action方法，显示订单明细
        public IActionResult OrderDetails(int id)
        {
            // 根据ID查询订单明细
            var orderDetail = _context.BudgetCharts.FirstOrDefault(budgetChart => budgetChart.BudgetChartId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return View(orderDetail);
        }
    }
}
