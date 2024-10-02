using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingPlanningReport.Models;

namespace WeddingPlanningReport.Controllers
{
    public class DishesController : Controller
    {
        private readonly WeddingPlanningContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public DishesController(WeddingPlanningContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: Dishes/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: Dishes/IndexJson
        public JsonResult IndexJson()
        {
            return Json(_context.Dishes);
        }

        // GET: Dishes/Index
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    // 如果有搜尋條件，將其儲存到 ViewData 供視圖顯示
        //    ViewData["CurrentFilter"] = searchString;

        //    // 查詢所有車輛
        //    var dishes = from d in _context.Dishes
        //               select d;

        //    // 如果搜尋字串不為空，篩選車輛名稱包含搜尋字串的車輛
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        dishes = dishes.Where(s => s.DishesName.Contains(searchString) ||
        //    s.DishesSort.Contains(searchString) || s.DishesDescription.Contains(searchString)||
        //    s.PricePerTable.ToString().Contains(searchString));
        //    }

        //    return View(await dishes.ToListAsync());
        //}

        // GET: Dishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .FirstOrDefaultAsync(m => m.DishesId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // GET: Dishes/Create
        public IActionResult Create()
        {
            var dish = new Dish(); // 確保創建一個新的模型實例
            return View(dish);
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishesId,ShopId,DishesName,PricePerTable,DishesDescription,DishesImg,DishesSort")] Dish dish,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string fileName = file.FileName;
                        string productPath = Path.Combine(wwwRootPath, "Dish1");

                        if (!Directory.Exists(productPath))
                        {
                            Directory.CreateDirectory(productPath);
                        }

                        // 防止檔案名衝突處理
                        string filePath = Path.Combine(productPath, fileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            string fileExtension = Path.GetExtension(fileName);
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                            fileName = $"{fileNameWithoutExtension}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";
                            filePath = Path.Combine(productPath, fileName);
                        }


                        // 儲存新圖片
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        dish.DishesImg = fileName;
                    }

                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.DishesId))
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
            return View(dish);
        }

        // GET: Dishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishesId,ShopId,DishesName,PricePerTable,DishesDescription,DishesImg,DishesSort")] Dish dish, IFormFile? file)
        {
            if (id != dish.DishesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string fileName = file.FileName;
                        string productPath = Path.Combine(wwwRootPath, "Dish1");

                        if (!Directory.Exists(productPath))
                        {
                            Directory.CreateDirectory(productPath);
                        }

                        // 防止檔案名衝突處理
                        string filePath = Path.Combine(productPath, fileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            string fileExtension = Path.GetExtension(fileName);
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                            fileName = $"{fileNameWithoutExtension}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";
                            filePath = Path.Combine(productPath, fileName);
                        }


                        // 儲存新圖片
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        dish.DishesImg = fileName;
                    }

                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.DishesId))
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

            return View(dish);
        }
        // GET: Dishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .FirstOrDefaultAsync(m => m.DishesId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            if (dish != null)
            {
                _context.Dishes.Remove(dish);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.DishesId == id);
        }
    }
}
