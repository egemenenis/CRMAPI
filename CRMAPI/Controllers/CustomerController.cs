using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/customer
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAllCustomers()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }

        // GET: api/customer/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound("Customer not found.");
            return Ok(customer);
        }

        // POST: api/customer
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCustomer([FromBody] CustomerModel customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        // PUT: api/customer/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerModel customer)
        {
            var existingCustomer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (existingCustomer == null)
                return NotFound("Customer not found.");

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Address = customer.Address;
            existingCustomer.DateOfBirth = customer.DateOfBirth;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound("Customer not found.");

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}