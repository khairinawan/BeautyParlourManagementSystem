using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeautyParlourManagementAPI.Data;
using BeautyParlourManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyParlourManagementAPI.Controllers
{
    [Route("[controller]")]
    public class AppointmentsController : Controller
    {
        private readonly BeautyParlourManagementContext _context;

        public AppointmentsController(BeautyParlourManagementContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments.ToListAsync();
            return View(appointments); // Returns the list view of appointments
        }

        // GET: Appointments/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment); // Returns the details view of a specific appointment
        }

        // GET: Appointments/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(); // Returns the view for creating a new appointment
        }

        // POST:  Appointments/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }


        // GET: Appointments/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment); // Returns the edit view for a specific appointment
        }

        // POST: Appointments/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Appointment appointment)
        {
            if (id != appointment.AppointmentID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index)); // Redirects to the appointment list
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment); // Returns the delete confirmation view
        }

        // POST: Appointments/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index)); // Redirects to the appointment list
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentID == id);
        }
    }
}