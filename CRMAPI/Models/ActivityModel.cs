namespace CRMAPI.Models
{
    public class ActivityModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}
