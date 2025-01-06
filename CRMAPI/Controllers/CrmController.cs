using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using System.Linq;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrmController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CrmController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/crm
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetCrmData()
        {
            var data = _context.CrmData.ToList();
            return Ok(data);
        }

        // GET: api/crm/stats
        [HttpGet("stats")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCrmStatistics()
        {
            var totalCustomers = _context.CrmData.Count();
            var activeCustomers = _context.CrmData.Count(c => !string.IsNullOrEmpty(c.CustomerEmail));
            var lastAddedCustomer = _context.CrmData.OrderByDescending(c => c.Id).FirstOrDefault();

            var stats = new
            {
                TotalCustomers = totalCustomers,
                ActiveCustomers = activeCustomers,
                LastAddedCustomer = lastAddedCustomer
            };

            return Ok(stats);
        }

        // GET: api/crm/latest
        [HttpGet("latest")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetLatestCrmData()
        {
            var latestCustomers = _context.CrmData.OrderByDescending(c => c.Id).Take(5).ToList();
            return Ok(latestCustomers);
        }

        // POST: api/crm
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCrmData([FromBody] CrmDataModel crmData)
        {
            if (crmData == null)
            {
                return BadRequest("CRM data is required.");
            }

            _context.CrmData.Add(crmData);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCrmData), new { id = crmData.Id }, crmData);
        }

        // PUT: api/crm/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCrmData(int id, [FromBody] CrmDataModel updatedCrmData)
        {
            if (id != updatedCrmData.Id)
            {
                return BadRequest("CRM ID mismatch.");
            }

            var existingCrmData = _context.CrmData.Find(id);
            if (existingCrmData == null)
            {
                return NotFound();
            }

            existingCrmData.CustomerName = updatedCrmData.CustomerName;
            existingCrmData.CustomerEmail = updatedCrmData.CustomerEmail;
            existingCrmData.Address = updatedCrmData.Address;
            existingCrmData.IsActive = updatedCrmData.IsActive;
            existingCrmData.CustomerType = updatedCrmData.CustomerType;

            _context.CrmData.Update(existingCrmData);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/crm/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCrmData(int id)
        {
            var crmData = _context.CrmData.Find(id);
            if (crmData == null)
            {
                return NotFound();
            }

            _context.CrmData.Remove(crmData);
            _context.SaveChanges();

            return NoContent();
        }
    }
}