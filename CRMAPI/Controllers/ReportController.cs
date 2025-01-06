using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using Microsoft.AspNetCore.Authorization;
using CRMAPI.Data;
using System.Linq;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/report
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetReports()
        {
            var reports = _context.Reports.ToList();
            return Ok(reports);
        }

        // GET: api/report/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetReportById(int id)
        {
            var report = _context.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        // POST: api/report
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateReport([FromBody] ReportModel report)
        {
            if (report == null)
            {
                return BadRequest("Report data is required.");
            }

            _context.Reports.Add(report);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetReportById), new { id = report.Id }, report);
        }

        // PUT: api/report/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateReport(int id, [FromBody] ReportModel updatedReport)
        {
            if (id != updatedReport.Id)
            {
                return BadRequest("Report ID mismatch.");
            }

            var existingReport = _context.Reports.Find(id);
            if (existingReport == null)
            {
                return NotFound();
            }

            existingReport.ReportTitle = updatedReport.ReportTitle;
            existingReport.Data = updatedReport.Data;
            existingReport.GeneratedDate = updatedReport.GeneratedDate;

            _context.Reports.Update(existingReport);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/report/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteReport(int id)
        {
            var report = _context.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Reports.Remove(report);
            _context.SaveChanges();

            return NoContent();
        }
    }
}