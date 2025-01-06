using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReminderController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/reminder
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult CreateReminder([FromBody] ReminderModel reminder)
        {
            if (reminder == null || string.IsNullOrEmpty(reminder.Message) || reminder.ReminderDate == default)
            {
                return BadRequest("Reminder data is required and must contain a valid message and date.");
            }

            _context.Reminders.Add(reminder);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetReminderById), new { id = reminder.Id }, reminder);
        }

        // GET: api/reminder/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetReminderById(int id)
        {
            var reminder = _context.Reminders.Find(id);
            if (reminder == null)
            {
                return NotFound();
            }
            return Ok(reminder);
        }

        // GET: api/reminder
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAllReminders()
        {
            var reminders = _context.Reminders.ToList();
            if (reminders.Count == 0)
            {
                return NotFound("No reminders found.");
            }
            return Ok(reminders);
        }

        // GET: api/reminder/user/{userId}
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUserReminders(int userId)
        {
            var reminders = _context.Reminders.Where(r => r.UserId == userId).ToList();
            if (reminders.Count == 0)
            {
                return NotFound("No reminders found for this user.");
            }
            return Ok(reminders);
        }

        // POST: api/reminder/send-reminders
        [HttpPost("send-reminders")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SendReminders()
        {
            var reminders = await _context.Reminders
                .Where(r => r.ReminderDate <= DateTime.Now && !r.IsSent)
                .ToListAsync();

            if (reminders.Count == 0)
            {
                return Ok("No reminders to send.");
            }

            foreach (var reminder in reminders)
            {
                var user = await _context.Users.FindAsync(reminder.UserId);
                if (user != null)
                {
                    // Placeholder for sending the reminder, e.g., by email or SMS
                }

                reminder.IsSent = true;
                _context.Reminders.Update(reminder);
            }

            await _context.SaveChangesAsync();

            return Ok("Reminders sent successfully.");
        }

        // DELETE: api/reminder/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult DeleteReminder(int id)
        {
            var reminder = _context.Reminders.Find(id);
            if (reminder == null)
            {
                return NotFound();
            }

            _context.Reminders.Remove(reminder);
            _context.SaveChanges();

            return NoContent();
        }

        // PUT: api/reminder/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult UpdateReminder(int id, [FromBody] ReminderModel reminder)
        {
            if (id != reminder.Id)
            {
                return BadRequest("Reminder ID mismatch.");
            }

            var existingReminder = _context.Reminders.Find(id);
            if (existingReminder == null)
            {
                return NotFound();
            }

            existingReminder.Message = reminder.Message;
            existingReminder.ReminderDate = reminder.ReminderDate;
            existingReminder.IsSent = reminder.IsSent;

            _context.Reminders.Update(existingReminder);
            _context.SaveChanges();

            return NoContent();
        }
    }
}