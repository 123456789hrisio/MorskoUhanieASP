using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelMorskoUhanie.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HotelMorskoUhanie.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ReservationsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservations.Include(r => r.Rooms).Include(r => r.Users);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Rooms)
                .Include(r => r.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["RoomsId"] = new SelectList(_context.Rooms, "Id", "Name");
            //ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }
        public async Task<IActionResult> CreateWithRoomId(int roomId, DateTime comeDate, DateTime leaveDate )
        {
            var currentRoom = await _context.Rooms.FirstOrDefaultAsync(z => z.Id == roomId);
            Reservation reservation = new Reservation();
            //order.ProductsId = productId;
            // productId = order.ProductsId;
            reservation.RoomsId = roomId;
            reservation.ComeInDate = comeDate;
            reservation.LeaveDate = leaveDate;
            reservation.UsersId = _userManager.GetUserId(User);
            var price = currentRoom.PricePerDay;
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComeInDate,LeaveDate,RoomsId")] Reservation reservation)
        {
            reservation.DateModified = DateTime.Now;
            reservation.UsersId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomsId"] = new SelectList(_context.Rooms, "Id", "Name", reservation.RoomsId);
            //ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Id", reservation.UsersId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["RoomsId"] = new SelectList(_context.Rooms, "Id", "Name", reservation.RoomsId);
            //ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Id", reservation.UsersId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ComeInDate,LeaveDate,RoomsId,DateModified")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    reservation.UsersId = _userManager.GetUserId(User);
                    reservation.DateModified = DateTime.Now;
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["RoomsId"] = new SelectList(_context.Rooms, "Id", "Name", reservation.RoomsId);
            //ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Id", reservation.UsersId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Rooms)
                .Include(r => r.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
