using Microsoft.AspNetCore.Mvc;
using CRMAPI.Models;
using CRMAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace CRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CampaignController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/campaign
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetCampaigns()
        {
            var campaigns = _context.Campaigns.ToList();
            return Ok(campaigns);
        }

        // GET: api/campaign/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetCampaignById(int id)
        {
            var campaign = _context.Campaigns.Find(id);
            if (campaign == null)
            {
                return NotFound();
            }
            return Ok(campaign);
        }

        // POST: api/campaign
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCampaign([FromBody] CampaignModel campaign)
        {
            _context.Campaigns.Add(campaign);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCampaigns), new { id = campaign.Id }, campaign);
        }

        // PUT: api/campaign/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCampaign(int id, [FromBody] CampaignModel campaign)
        {
            var existingCampaign = _context.Campaigns.Find(id);
            if (existingCampaign == null)
            {
                return NotFound();
            }

            existingCampaign.CampaignName = campaign.CampaignName;
            existingCampaign.Description = campaign.Description;
            existingCampaign.StartDate = campaign.StartDate;
            existingCampaign.EndDate = campaign.EndDate;
            existingCampaign.Status = campaign.Status;
            existingCampaign.Budget = campaign.Budget;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/campaign/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCampaign(int id)
        {
            var campaign = _context.Campaigns.Find(id);
            if (campaign == null)
            {
                return NotFound();
            }
            _context.Campaigns.Remove(campaign);
            _context.SaveChanges();
            return NoContent();
        }
    }
}