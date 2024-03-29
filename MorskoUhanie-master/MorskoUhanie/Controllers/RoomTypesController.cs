﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MorskoUhanie.Data;

namespace MorskoUhanie.Controllers
{
    public class RoomTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoomTypes
        public async Task<IActionResult> Index()
        {
            return _context.RoomType != null ?
                        View(await _context.RoomType.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.RoomType'  is null.");
        }

        // GET: RoomTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RoomType == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        // GET: RoomTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] RoomType roomType)
        {
            roomType.Register = DateTime.Now;
            if (ModelState.IsValid)
            {
                return View(roomType);
            }
            _context.RoomType.Add(roomType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: RoomTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RoomType == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomType.FindAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }

        // POST: RoomTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] RoomType roomType)
        {
            if (id != roomType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                return View(roomType);
            }
            try
            {
                _context.Update(roomType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomTypeExists(roomType.Id))
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

        // GET: RoomTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RoomType == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        // POST: RoomTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RoomType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RoomType'  is null.");
            }
            var roomType = await _context.RoomType.FindAsync(id);
            if (roomType != null)
            {
                _context.RoomType.Remove(roomType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomTypeExists(int id)
        {
            return (_context.RoomType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
