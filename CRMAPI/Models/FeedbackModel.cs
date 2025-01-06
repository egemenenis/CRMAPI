namespace CRMAPI.Models
{
    public class FeedbackModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string FeedbackContent { get; set; }
        public DateTime FeedbackDate { get; set; }
    }
}
