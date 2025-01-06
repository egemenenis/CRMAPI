using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaymentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/payment
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetPayments()
        {
            var payments = _context.Payments.ToList();
            return Ok(payments);
        }

        // POST: api/payment
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreatePayment([FromBody] PaymentModel payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPayments), new { id = payment.Id }, payment);
        }

        // GET: api/payment/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetPaymentById(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        // PUT: api/payment/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePayment(int id, [FromBody] PaymentModel payment)
        {
            var existingPayment = _context.Payments.Find(id);
            if (existingPayment == null)
            {
                return NotFound();
            }

            existingPayment.AmountPaid = payment.AmountPaid;
            existingPayment.PaymentDate = payment.PaymentDate;
            existingPayment.PaymentMethod = payment.PaymentMethod;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/payment/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeletePayment(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }
            _context.Payments.Remove(payment);
            _context.SaveChanges();
            return NoContent();
        }
    }
}