using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelMorskoUhanie.Data;

namespace HotelMorskoUhanie.Controllers
{
    public class RoomNumbersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomNumbersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoomNumbers
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoomNumbers.ToListAsync());
        }

        // GET: RoomNumbers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomNumber = await _context.RoomNumbers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomNumber == null)
            {
                return NotFound();
            }

            return View(roomNumber);
        }

        // GET: RoomNumbers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomNumbers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomNumberName,DateModified")] RoomNumber roomNumber)
        {
            if (ModelState.IsValid)
            {
                roomNumber.DateModified = DateTime.Now;
                _context.RoomNumbers.Add(roomNumber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roomNumber);
        }

        // GET: RoomNumbers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomNumber = await _context.RoomNumbers.FindAsync(id);
            if (roomNumber == null)
            {
                return NotFound();
            }
            return View(roomNumber);
        }

        // POST: RoomNumbers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomNumberName,DateModified")] RoomNumber roomNumber)
        {
            if (id != roomNumber.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    roomNumber.DateModified = DateTime.Now;
                    _context.RoomNumbers.Update(roomNumber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomNumberExists(roomNumber.Id))
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
            return View(roomNumber);
        }

        // GET: RoomNumbers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomNumber = await _context.RoomNumbers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomNumber == null)
            {
                return NotFound();
            }

            return View(roomNumber);
        }

        // POST: RoomNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomNumber = await _context.RoomNumbers.FindAsync(id);
            if (roomNumber != null)
            {
                _context.RoomNumbers.Remove(roomNumber);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomNumberExists(int id)
        {
            return _context.RoomNumbers.Any(e => e.Id == id);
        }
    }
}
