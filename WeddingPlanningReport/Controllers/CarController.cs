using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingPlanningReport.Models;

namespace WeddingPlanningReport.Controllers
{
    public class CarController : Controller
    {
        private readonly WeddingPlanningContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // 使用建構子注入 DbContext
        public CarController(WeddingPlanningContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: Car/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: Car/IndexJson
        public JsonResult IndexJson()
        {
            return Json(_context.Cars);
        }

        //// GET: Car/Index
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    // 如果有搜尋條件，將其儲存到 ViewData 供視圖顯示
        //    ViewData["CurrentFilter"] = searchString;

        //    // 查詢所有車輛
        //    var cars = from c in _context.Cars
        //               select c;

        //    // 如果搜尋字串不為空，篩選車輛名稱包含搜尋字串的車輛
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        cars = cars.Where(s => s.CarName.Contains(searchString) ||
        //    s.CarStatus.Contains(searchString) ||
        //    s.RentalPerDay.ToString().Contains(searchString) || s.ShopId.ToString().Contains(searchString));
        //    }

        //    // 返回篩選後的結果，而不是重新撈取所有資料
        //    return View(await cars.ToListAsync());
        //}

        // GET: Car/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            var car = new Car(); // 確保創建一個新的模型實例
            return View(car);
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,ShopId,CarName,PassengerCapacity,RentalPerDay,CarStatus,CarImg,CarDetail,Quantity")] Car car, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string fileName = file.FileName;
                        string productPath = Path.Combine(wwwRootPath, "Car1");

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

                        car.CarImg = fileName;
                    }

                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
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
            return View(car);
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,ShopId,CarName,PassengerCapacity,RentalPerDay,CarStatus,CarImg,CarDetail,Quantity")] Car car, IFormFile? file)
        {
            if (id != car.CarId)
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
                        string productPath = Path.Combine(wwwRootPath, "Car1");

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

                        car.CarImg = fileName;
                    }

                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
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

            return View(car);
        }

        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }
    }
}
