using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FeedbackController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/feedback
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetFeedbacks()
        {
            var feedbacks = _context.Feedbacks.ToList();
            return Ok(feedbacks);
        }

        // POST: api/feedback
        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult CreateFeedback([FromBody] FeedbackModel feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetFeedbacks), new { id = feedback.Id }, feedback);
        }

        // GET: api/feedback/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetFeedbackById(int id)
        {
            var feedback = _context.Feedbacks.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return Ok(feedback);
        }

        // PUT: api/feedback/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateFeedback(int id, [FromBody] FeedbackModel feedback)
        {
            var existingFeedback = _context.Feedbacks.Find(id);
            if (existingFeedback == null)
            {
                return NotFound();
            }

            existingFeedback.FeedbackContent = feedback.FeedbackContent;
            existingFeedback.FeedbackDate = feedback.FeedbackDate;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/feedback/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteFeedback(int id)
        {
            var feedback = _context.Feedbacks.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }
            _context.Feedbacks.Remove(feedback);
            _context.SaveChanges();
            return NoContent();
        }
    }
}