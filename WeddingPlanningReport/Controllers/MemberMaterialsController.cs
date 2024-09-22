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
    public class MemberMaterialsController : Controller
    {
        private readonly WeddingPlanningContext _context;

        public MemberMaterialsController(WeddingPlanningContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Edit(int id, [Bind("MemberMaterialId,MemberId,MemberImgName,EstimatedLength,EstimatedWidth,IsDelete")] MemberMaterial memberMaterial)
        {
            if (id != memberMaterial.MemberMaterialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                _context.MemberMaterials.Remove(memberMaterial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberMaterialExists(int id)
        {
            return _context.MemberMaterials.Any(e => e.MemberMaterialId == id);
        }
    }
}
