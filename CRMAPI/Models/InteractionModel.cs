namespace CRMAPI.Models
{
    public class InteractionModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string InteractionType { get; set; }
        public DateTime InteractionDate { get; set; }
        public string Notes { get; set; }
    }
}
