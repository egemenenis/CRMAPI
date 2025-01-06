namespace CRMAPI.Models
{
    public class ReminderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool IsSent { get; set; }
    }
}
