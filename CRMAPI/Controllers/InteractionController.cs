using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InteractionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/interaction
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetInteractions()
        {
            var interactions = _context.Interactions.ToList();
            return Ok(interactions);
        }

        // POST: api/interaction
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult CreateInteraction([FromBody] InteractionModel interaction)
        {
            _context.Interactions.Add(interaction);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetInteractions), new { id = interaction.Id }, interaction);
        }

        // GET: api/interaction/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetInteractionById(int id)
        {
            var interaction = _context.Interactions.Find(id);
            if (interaction == null)
            {
                return NotFound();
            }
            return Ok(interaction);
        }

        // PUT: api/interaction/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult UpdateInteraction(int id, [FromBody] InteractionModel interaction)
        {
            var existingInteraction = _context.Interactions.Find(id);
            if (existingInteraction == null)
            {
                return NotFound();
            }

            existingInteraction.InteractionType = interaction.InteractionType;
            existingInteraction.InteractionDate = interaction.InteractionDate;
            existingInteraction.Notes = interaction.Notes;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/interaction/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteInteraction(int id)
        {
            var interaction = _context.Interactions.Find(id);
            if (interaction == null)
            {
                return NotFound();
            }
            _context.Interactions.Remove(interaction);
            _context.SaveChanges();
            return NoContent();
        }
    }
}