using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPipelineController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesPipelineController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/salespipeline
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetPipeline()
        {
            var pipeline = _context.SalesPipelines.ToList();
            return Ok(pipeline);
        }

        // POST: api/salespipeline
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreatePipeline([FromBody] SalesPipelineModel pipeline)
        {
            _context.SalesPipelines.Add(pipeline);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPipeline), new { id = pipeline.Id }, pipeline);
        }

        // GET: api/salespipeline/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetPipelineById(int id)
        {
            var pipeline = _context.SalesPipelines.Find(id);
            if (pipeline == null)
            {
                return NotFound();
            }
            return Ok(pipeline);
        }

        // PUT: api/salespipeline/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePipeline(int id, [FromBody] SalesPipelineModel pipeline)
        {
            var existingPipeline = _context.SalesPipelines.Find(id);
            if (existingPipeline == null)
            {
                return NotFound();
            }

            existingPipeline.Stage = pipeline.Stage;
            existingPipeline.DateEntered = pipeline.DateEntered;
            existingPipeline.EstimatedValue = pipeline.EstimatedValue;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/salespipeline/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeletePipeline(int id)
        {
            var pipeline = _context.SalesPipelines.Find(id);
            if (pipeline == null)
            {
                return NotFound();
            }
            _context.SalesPipelines.Remove(pipeline);
            _context.SaveChanges();
            return NoContent();
        }
    }
}