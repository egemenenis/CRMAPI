namespace CRMAPI.Models
{
    public class LeadModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public string LeadSource { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
