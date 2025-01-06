namespace CRMAPI.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CustomerId { get; set; }
    }
}
