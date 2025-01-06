using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ticket
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetTickets()
        {
            var tickets = _context.Tickets.ToList();
            return Ok(tickets);
        }

        // POST: api/ticket
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult CreateTicket([FromBody] TicketModel ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTickets), new { id = ticket.Id }, ticket);
        }

        // GET: api/ticket/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetTicketById(int id)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        // PUT: api/ticket/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateTicket(int id, [FromBody] TicketModel ticket)
        {
            var existingTicket = _context.Tickets.Find(id);
            if (existingTicket == null)
            {
                return NotFound();
            }

            existingTicket.Subject = ticket.Subject;
            existingTicket.Description = ticket.Description;
            existingTicket.Status = ticket.Status;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/ticket/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteTicket(int id)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
            return NoContent();
        }
    }
}