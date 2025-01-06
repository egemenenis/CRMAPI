using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/document
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetDocuments()
        {
            var documents = _context.Documents.ToList();
            return Ok(documents);
        }

        // GET: api/document/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetDocumentById(int id)
        {
            var document = _context.Documents.Find(id);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }

        // POST: api/document
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateDocument([FromBody] DocumentModel document)
        {
            if (document == null)
            {
                return BadRequest("Document data is required.");
            }

            document.CreatedDate = DateTime.Now;
            _context.Documents.Add(document);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetDocumentById), new { id = document.Id }, document);
        }

        // PUT: api/document/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateDocument(int id, [FromBody] DocumentModel document)
        {
            if (id != document.Id)
            {
                return BadRequest("Document ID mismatch.");
            }

            var existingDocument = _context.Documents.Find(id);
            if (existingDocument == null)
            {
                return NotFound();
            }

            existingDocument.DocumentTitle = document.DocumentTitle;
            existingDocument.DocumentDescription = document.DocumentDescription;
            existingDocument.LastUpdatedDate = DateTime.Now;

            _context.Documents.Update(existingDocument);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/document/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteDocument(int id)
        {
            var document = _context.Documents.Find(id);
            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);
            _context.SaveChanges();

            return NoContent();
        }
    }
}