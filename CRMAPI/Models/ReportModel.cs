namespace CRMAPI.Models
{
    public class ReportModel
    {
        public int Id { get; set; }
        public string ReportTitle { get; set; }
        public string Data { get; set; }
        public DateTime GeneratedDate { get; set; }
        public string GeneratedBy { get; set; }
    }
}
