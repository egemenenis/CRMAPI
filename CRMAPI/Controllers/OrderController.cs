using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/order
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAllOrders()
        {
            var orders = _context.Orders.ToList();
            return Ok(orders);
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetOrderById(int id)
        {
            var order = _context.Orders.SingleOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound("Order not found.");
            return Ok(order);
        }

        // POST: api/order
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateOrder([FromBody] OrderModel order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        // PUT: api/order/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderModel order)
        {
            var existingOrder = _context.Orders.SingleOrDefault(o => o.Id == id);
            if (existingOrder == null)
                return NotFound("Order not found.");

            existingOrder.CustomerId = order.CustomerId;
            existingOrder.OrderDetails = order.OrderDetails;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.TotalAmount = order.TotalAmount;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.SingleOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound("Order not found.");

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return NoContent();
        }
    }
}