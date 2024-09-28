using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WeddingPlanningReport.Models;

namespace WeddingPlanningReport.Controllers
{
    public class MemberMaterialsController : Controller
    {
        private readonly WeddingPlanningContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MemberMaterialsController(WeddingPlanningContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: MemberMaterials
        public async Task<IActionResult> Index()
        {
            return View(await _context.MemberMaterials.ToListAsync());
        }

        // GET: MemberMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberMaterial = await _context.MemberMaterials
                .FirstOrDefaultAsync(m => m.MemberMaterialId == id);
            if (memberMaterial == null)
            {
                return NotFound();
            }

            return View(memberMaterial);
        }

        // GET: MemberMaterials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberMaterialId,MemberId,MemberImgName,EstimatedLength,EstimatedWidth,IsDelete")] MemberMaterial memberMaterial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memberMaterial);
        }

        // GET: MemberMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberMaterial = await _context.MemberMaterials.FindAsync(id);
            if (memberMaterial == null)
            {
                return NotFound();
            }
            return View(memberMaterial);
        }

        // POST: MemberMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberMaterialId,MemberId,MemberImgName,EstimatedLength,EstimatedWidth,IsDelete")] MemberMaterial memberMaterial, IFormFile? file)
        {
            if (id != memberMaterial.MemberMaterialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = memberMaterial.MemberImgName; // 保留现有的图片名
                    if (file != null)
                    {
                        string newFileName = file.FileName;
                        string productPath = Path.Combine(wwwRootPath, @"圖片與圖層\圖片\會員提供圖");

                        string filePath = Path.Combine(productPath, newFileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            // 檔案已存在，這裡可以根據需求修改，例如在檔名後加上時間戳
                            string fileExtension = Path.GetExtension(newFileName);
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(newFileName);
                            newFileName = $"{fileNameWithoutExtension}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";
                            filePath = Path.Combine(productPath, newFileName);
                        }

                        string oldFilePath = Path.Combine(productPath, memberMaterial.MemberImgName);
                        if (!string.IsNullOrEmpty(memberMaterial.MemberImgName) && System.IO.File.Exists(oldFilePath))
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
                    memberMaterial.MemberImgName = fileName;
                    _context.Update(memberMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberMaterialExists(memberMaterial.MemberMaterialId))
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
            return View(memberMaterial);
        }

        // GET: MemberMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberMaterial = await _context.MemberMaterials
                .FirstOrDefaultAsync(m => m.MemberMaterialId == id);
            if (memberMaterial == null)
            {
                return NotFound();
            }

            return View(memberMaterial);
        }

        // POST: MemberMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberMaterial = await _context.MemberMaterials.FindAsync(id);
            if (memberMaterial != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string productPath = Path.Combine(wwwRootPath, @"圖片與圖層\圖片\會員提供圖");
                if (!string.IsNullOrEmpty(memberMaterial.MemberImgName))
                {
                    string filePath = Path.Combine(productPath, memberMaterial.MemberImgName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                _context.MemberMaterials.Remove(memberMaterial);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MemberMaterialExists(int id)
        {
            return _context.MemberMaterials.Any(e => e.MemberMaterialId == id);
        }
    }
}
