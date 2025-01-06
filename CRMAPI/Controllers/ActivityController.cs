using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActivityController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/activity
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAllActivities()
        {
            var activities = _context.Activities.ToList();
            return Ok(activities);
        }

        // GET: api/activity/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetActivityById(int id)
        {
            var activity = _context.Activities.Find(id);
            if (activity == null)
            {
                return NotFound("Activity not found.");
            }
            return Ok(activity);
        }

        // POST: api/activity
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult CreateActivity([FromBody] ActivityModel activity)
        {
            if (activity == null)
            {
                return BadRequest("Activity data is required.");
            }

            _context.Activities.Add(activity);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAllActivities), new { id = activity.Id }, activity);
        }

        // PUT: api/activity/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult UpdateActivity(int id, [FromBody] ActivityModel updatedActivity)
        {
            if (id != updatedActivity.Id)
            {
                return BadRequest("Activity ID mismatch.");
            }

            var existingActivity = _context.Activities.Find(id);
            if (existingActivity == null)
            {
                return NotFound();
            }

            existingActivity.UserId = updatedActivity.UserId;
            existingActivity.CustomerId = updatedActivity.CustomerId;
            existingActivity.ActivityDescription = updatedActivity.ActivityDescription;
            existingActivity.ActivityDate = updatedActivity.ActivityDate;

            _context.Activities.Update(existingActivity);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/activity/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteActivity(int id)
        {
            var activity = _context.Activities.Find(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            _context.SaveChanges();

            return NoContent();
        }
    }
}