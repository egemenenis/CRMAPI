using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeadController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/lead
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetLeads()
        {
            var leads = _context.Leads.ToList();
            return Ok(leads);
        }

        // POST: api/lead
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateLead([FromBody] LeadModel lead)
        {
            _context.Leads.Add(lead);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetLeads), new { id = lead.Id }, lead);
        }

        // GET: api/lead/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetLeadById(int id)
        {
            var lead = _context.Leads.Find(id);
            if (lead == null)
            {
                return NotFound();
            }
            return Ok(lead);
        }

        // PUT: api/lead/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateLead(int id, [FromBody] LeadModel lead)
        {
            var existingLead = _context.Leads.Find(id);
            if (existingLead == null)
            {
                return NotFound();
            }

            existingLead.Name = lead.Name;
            existingLead.ContactInfo = lead.ContactInfo;
            existingLead.LeadSource = lead.LeadSource;
            existingLead.Status = lead.Status;
            existingLead.DateCreated = lead.DateCreated;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/lead/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteLead(int id)
        {
            var lead = _context.Leads.Find(id);
            if (lead == null)
            {
                return NotFound();
            }
            _context.Leads.Remove(lead);
            _context.SaveChanges();
            return NoContent();
        }
    }
}