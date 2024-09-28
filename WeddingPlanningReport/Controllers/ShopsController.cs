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
    public class ShopsController : Controller
    {
        private readonly WeddingPlanningContext _context;

        public ShopsController(WeddingPlanningContext context)
        {
            _context = context;
        }


        // GET: Shops
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<JsonResult> IndexJson()       
        {
            return Json(_context.Shops);
        }

        // GET: Shops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop = await _context.Shops
                .FirstOrDefaultAsync(m => m.ShopId == id);
            if (shop == null)
            {
                return NotFound();
            }

            return View(shop);
        }

        // GET: Shops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shop shop, string? ShopImgBase64, string? ShopLogoBase64)
        {
            if (ModelState.IsValid)
            {
                // 處理 ShopImg 上傳
                if (!string.IsNullOrEmpty(ShopImgBase64))
                {
                    try
                    {
                        var base64Data = ShopImgBase64.Split(',')[1];
                        var imageBytes = Convert.FromBase64String(base64Data);

                        // 判斷圖片格式 (jpg 或 png)
                        var fileExtension = ShopImgBase64.Contains("image/png") ? ".png" : ".jpg";
                        var shopImgFileName = $"{Guid.NewGuid()}{fileExtension}";

                        // 儲存至 ShopImg 資料夾
                        var shopImgFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ShopImg", shopImgFileName);
                        await System.IO.File.WriteAllBytesAsync(shopImgFilePath, imageBytes);

                        // 保存 ShopImg 的檔案名稱到資料庫
                        shop.ShopImg = shopImgFileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "ShopImg 圖片上傳失敗，請稍後再試。");
                        return View(shop);
                    }
                }
                else
                {
                    // 使用預設圖片
                    shop.ShopImg = "default.jpg";
                }

                // 處理 ShopLogo 上傳
                if (!string.IsNullOrEmpty(ShopLogoBase64))
                {
                    try
                    {
                        var base64Data = ShopLogoBase64.Split(',')[1];
                        var imageBytes = Convert.FromBase64String(base64Data);

                        // 判斷圖片格式 (jpg 或 png)
                        var fileExtension = ShopLogoBase64.Contains("image/png") ? ".png" : ".jpg";
                        var shopLogoFileName = $"{Guid.NewGuid()}{fileExtension}";

                        // 儲存至 ShopLogo 資料夾
                        var shopLogoFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ShopLogo", shopLogoFileName);
                        await System.IO.File.WriteAllBytesAsync(shopLogoFilePath, imageBytes);

                        // 保存 ShopLogo 的檔案名稱到資料庫
                        shop.ShopLogo = shopLogoFileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "ShopLogo 圖片上傳失敗，請稍後再試。");
                        return View(shop);
                    }
                }
                else
                {
                    // 使用預設圖片
                    shop.ShopLogo = "default.jpg";
                }

                // 儲存其他資料
                _context.Add(shop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(shop);
        }

        // GET: Shops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop = await _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }
            return View(shop);
        }

        // POST: Shops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Shop shop, string? ShopImgBase64, string? ShopLogoBase64)
        {
            if (id != shop.ShopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // 取得現有的 Shop 資料
                var existingShop = await _context.Shops.AsNoTracking().FirstOrDefaultAsync(s => s.ShopId == id);
                if (existingShop == null)
                {
                    return NotFound();
                }

                // 處理 ShopImg 上傳
                if (!string.IsNullOrEmpty(ShopImgBase64))
                {
                    try
                    {
                        var base64Data = ShopImgBase64.Split(',')[1];
                        var imageBytes = Convert.FromBase64String(base64Data);
                        var shopImgFileName = $"{Guid.NewGuid()}.jpg";
                        var shopImgFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ShopImg", shopImgFileName);
                        await System.IO.File.WriteAllBytesAsync(shopImgFilePath, imageBytes);
                        shop.ShopImg = shopImgFileName; // 更新資料庫中的圖片檔名
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "ShopImg 圖片上傳失敗，請稍後再試。");
                        return View(shop);
                    }
                }
                else
                {
                    // 如果沒有上傳新圖片，保留原有的圖片名稱
                    shop.ShopImg = existingShop.ShopImg;
                }

                // 處理 ShopLogo 上傳
                if (!string.IsNullOrEmpty(ShopLogoBase64))
                {
                    try
                    {
                        var base64Data = ShopLogoBase64.Split(',')[1];
                        var imageBytes = Convert.FromBase64String(base64Data);
                        var shopLogoFileName = $"{Guid.NewGuid()}.jpg";
                        var shopLogoFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ShopLogo", shopLogoFileName);
                        await System.IO.File.WriteAllBytesAsync(shopLogoFilePath, imageBytes);
                        shop.ShopLogo = shopLogoFileName; // 更新資料庫中的圖片檔名
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "ShopLogo 圖片上傳失敗，請稍後再試。");
                        return View(shop);
                    }
                }
                else
                {
                    // 如果沒有上傳新圖片，保留原有的圖片名稱
                    shop.ShopLogo = existingShop.ShopLogo;
                }

                // 更新其他欄位
                _context.Update(shop);
                await _context.SaveChangesAsync();

                // 更新相關資料表的 isDelete 欄位
                var isDeleteValue = shop.IsDelete;

                // 更新 cake 
                var cakes = await _context.Cakes.Where(c => c.ShopId == shop.ShopId).ToListAsync();
                foreach (var cake in cakes)
                {
                    cake.IsDelete = isDeleteValue;
                }
                _context.Cakes.UpdateRange(cakes);

                // 更新 car 
                var cars = await _context.Cars.Where(c => c.ShopId == shop.ShopId).ToListAsync();
                foreach (var car in cars)
                {
                    car.IsDelete = isDeleteValue;
                }
                _context.Cars.UpdateRange(cars);

                // 更新 venue 
                var venues = await _context.Venues.Where(v => v.ShopId == shop.ShopId).ToListAsync();
                foreach (var venue in venues)
                {
                    venue.IsDelete = isDeleteValue;
                }
                _context.Venues.UpdateRange(venues);

                // 更新 dishes 
                var dishes = await _context.Dishes.Where(d => d.ShopId == shop.ShopId).ToListAsync();
                foreach (var dish in dishes)
                {
                    dish.IsDelete = isDeleteValue;
                }
                _context.Dishes.UpdateRange(dishes);

                // 儲存所有變更
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(shop);
        }



        // GET: Shops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop = await _context.Shops
                .FirstOrDefaultAsync(m => m.ShopId == id);
            if (shop == null)
            {
                return NotFound();
            }

            return View(shop);
        }

        // POST: Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop != null)
            {
                _context.Shops.Remove(shop);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopExists(int id)
        {
            return _context.Shops.Any(e => e.ShopId == id);
        }

        

    }
}
