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
    public class ToDoesController : Controller
    {
        private readonly WeddingPlanningContext _context;

        public ToDoesController(WeddingPlanningContext context)
        {
            _context = context;
        }

        //// GET: ToDoes
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.ToDos.ToListAsync());
        //}


        public IActionResult Index()
        {
            // 獲取所有有待辦事項的會員
            var membersWithToDos = _context.Members
                .Where(m => _context.ToDos.Any(t => t.MemberId == m.MemberId))
                .ToList();

            return View(membersWithToDos);
        }

        public IActionResult GetToDosByMemberId(int memberId)
        {
            var toDos = _context.ToDos.Where(t => t.MemberId == memberId).ToList();
            return PartialView("_ToDosPartial", toDos);
        }


        // GET: ToDoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDos
                .FirstOrDefaultAsync(m => m.ToDoId == id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);


        }

        // GET: ToDoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToDoId,MemberId,ToDoName,ToDoDetail,ExpiringDate,Precedence,TaskStatus,PersonInCharge,TaskSort,RemindTime,AdditionalData,CreateToDoTime,UpdateToDoTime,RepetitiveTask")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDo);
        }

        // GET: ToDoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }
            return View(toDo);

        }

        // POST: ToDoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ToDoId,MemberId,ToDoName,ToDoDetail,ExpiringDate,Precedence,TaskStatus,PersonInCharge,TaskSort,RemindTime,AdditionalData,CreateToDoTime,UpdateToDoTime,RepetitiveTask")] ToDo toDo)
        {
            if (id != toDo.ToDoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoExists(toDo.ToDoId))
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
            return View(toDo);
        }

        // GET: ToDoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDos
                .FirstOrDefaultAsync(m => m.ToDoId == id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo != null)
            {
                _context.ToDos.Remove(toDo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoExists(int id)
        {
            return _context.ToDos.Any(e => e.ToDoId == id);
        }
    }
}
