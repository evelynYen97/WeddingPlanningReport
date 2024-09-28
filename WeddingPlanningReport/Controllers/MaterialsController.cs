using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WeddingPlanningReport.Models;

namespace WeddingPlanningReport.Controllers
{
    public class MaterialsController : Controller
    {
        private readonly WeddingPlanningContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;//0919新增

        public MaterialsController(WeddingPlanningContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Materials
        public async Task<IActionResult> Index()
        {
            return View(await _context.Materials.ToListAsync());
        }

        // GET: Materials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Materials
                .FirstOrDefaultAsync(m => m.MaterialId == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // GET: Materials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaterialId,ImageName,EstimatedL,EstimatedW")] Material material, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = "noimage.jpg"; // 保留现有的图片名
                if (file != null)
                {
                    //string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);為圖片生成一個唯一的檔名 (Guid.NewGuid())
                    string newFileName = file.FileName;
                    string productPath = Path.Combine(wwwRootPath, @"圖片與圖層\圖片\網站");

                    // 防止檔名衝突，如果檔案已存在，可以加後綴或處理邏輯
                    string filePath = Path.Combine(productPath, newFileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        // 檔案已存在，這裡可以根據需求修改，例如在檔名後加上時間戳
                        string fileExtension = Path.GetExtension(newFileName);
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(newFileName);
                        newFileName = $"{fileNameWithoutExtension}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";
                        filePath = Path.Combine(productPath, newFileName);
                    }
                    // 儲存圖片到指定路徑
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    fileName = newFileName;
                }
                material.ImageName = fileName;

                _context.Add(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(material);
        }

        // GET: Materials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaterialId,ImageName,EstimatedL,EstimatedW")] Material material,IFormFile? file)
        {
            if (id != material.MaterialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = material.ImageName; // 保留现有的图片名
                    if (file != null)
                    {
                        string newFileName = file.FileName;
                        string productPath = Path.Combine(wwwRootPath, @"圖片與圖層\圖片\網站");

                        string filePath = Path.Combine(productPath, newFileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            // 檔案已存在，這裡可以根據需求修改，例如在檔名後加上時間戳
                            string fileExtension = Path.GetExtension(newFileName);
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(newFileName);
                            newFileName = $"{fileNameWithoutExtension}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";
                            filePath = Path.Combine(productPath, newFileName);
                        }

                        string oldFilePath = Path.Combine(productPath, material.ImageName);
                        if (!string.IsNullOrEmpty(material.ImageName) && System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath); // 删除旧文件
                        }

                        // 儲存圖片到指定路徑
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        fileName = newFileName;
                    }
                    material.ImageName = fileName;
                    _context.Update(material);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialExists(material.MaterialId))
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
            return View(material);
        }

        // GET: Materials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Materials
                .FirstOrDefaultAsync(m => m.MaterialId == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string productPath = Path.Combine(wwwRootPath, @"圖片與圖層\圖片\網站");
                if (!string.IsNullOrEmpty(material.ImageName) && material.ImageName != "noimage.jpg")
                {
                    string filePath = Path.Combine(productPath, material.ImageName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                _context.Materials.Remove(material);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialExists(int id)
        {
            return _context.Materials.Any(e => e.MaterialId == id);
        }
    }
}
