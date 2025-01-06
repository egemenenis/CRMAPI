using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InvoiceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/invoice
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetInvoices()
        {
            var invoices = _context.Invoices.ToList();
            return Ok(invoices);
        }

        // POST: api/invoice
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateInvoice([FromBody] InvoiceModel invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetInvoices), new { id = invoice.Id }, invoice);
        }

        // GET: api/invoice/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetInvoiceById(int id)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        // PUT: api/invoice/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateInvoice(int id, [FromBody] InvoiceModel invoice)
        {
            var existingInvoice = _context.Invoices.Find(id);
            if (existingInvoice == null)
            {
                return NotFound();
            }

            existingInvoice.InvoiceDate = invoice.InvoiceDate;
            existingInvoice.TotalAmount = invoice.TotalAmount;
            existingInvoice.InvoiceStatus = invoice.InvoiceStatus;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/invoice/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteInvoice(int id)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound();
            }
            _context.Invoices.Remove(invoice);
            _context.SaveChanges();
            return NoContent();
        }
    }
}