using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/task
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetTasks()
        {
            var tasks = _context.Tasks.ToList();
            return Ok(tasks);
        }

        // POST: api/task
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateTask([FromBody] TaskModel task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
        }

        // GET: api/task/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetTaskById(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // PUT: api/task/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateTask(int id, [FromBody] TaskModel task)
        {
            var existingTask = _context.Tasks.Find(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.TaskTitle = task.TaskTitle;
            existingTask.Description = task.Description;
            existingTask.DueDate = task.DueDate;
            existingTask.AssignedTo = task.AssignedTo;
            existingTask.Status = task.Status;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/task/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return NoContent();
        }
    }
}