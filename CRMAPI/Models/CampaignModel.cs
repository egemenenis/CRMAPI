namespace CRMAPI.Models
{
    public class CampaignModel
    {
        public int Id { get; set; }
        public string CampaignName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public decimal Budget { get; set; }
    }
}
