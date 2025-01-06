namespace CRMAPI.Models
{
    public class SalesPipelineModel
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public string Stage { get; set; }
        public DateTime DateEntered { get; set; }
        public decimal EstimatedValue { get; set; }
    }
}
