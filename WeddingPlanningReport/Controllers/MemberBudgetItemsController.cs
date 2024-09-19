﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingPlanningReport.Models;

namespace WeddingPlanningReport.Controllers
{
    public class MemberBudgetItemsController : Controller
    {
        private readonly WeddingPlanningContext _context;

        public MemberBudgetItemsController(WeddingPlanningContext context)
        {
            _context = context;
        }

        // GET: MemberBudgetItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.MemberBudgetItems.ToListAsync());
        }

        // GET: MemberBudgetItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberBudgetItem = await _context.MemberBudgetItems
                .FirstOrDefaultAsync(m => m.BudgetItemId == id);
            if (memberBudgetItem == null)
            {
                return NotFound();
            }

            return View(memberBudgetItem);
        }

        // GET: MemberBudgetItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberBudgetItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BudgetItemId,MemberId,BudgetItemDetail,BudgetItemPrice,BudgetItemAmount,BudgetItemSubtotal,BudgetItemSort")] MemberBudgetItem memberBudgetItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberBudgetItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memberBudgetItem);
        }

        // GET: MemberBudgetItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberBudgetItem = await _context.MemberBudgetItems.FindAsync(id);
            if (memberBudgetItem == null)
            {
                return NotFound();
            }
            return View(memberBudgetItem);
        }

        // POST: MemberBudgetItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BudgetItemId,MemberId,BudgetItemDetail,BudgetItemPrice,BudgetItemAmount,BudgetItemSubtotal,BudgetItemSort")] MemberBudgetItem memberBudgetItem)
        {
            if (id != memberBudgetItem.BudgetItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberBudgetItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberBudgetItemExists(memberBudgetItem.BudgetItemId))
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
            return View(memberBudgetItem);
        }

        // GET: MemberBudgetItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberBudgetItem = await _context.MemberBudgetItems
                .FirstOrDefaultAsync(m => m.BudgetItemId == id);
            if (memberBudgetItem == null)
            {
                return NotFound();
            }

            return View(memberBudgetItem);
        }

        // POST: MemberBudgetItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberBudgetItem = await _context.MemberBudgetItems.FindAsync(id);
            if (memberBudgetItem != null)
            {
                _context.MemberBudgetItems.Remove(memberBudgetItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberBudgetItemExists(int id)
        {
            return _context.MemberBudgetItems.Any(e => e.BudgetItemId == id);
        }
    }
}