namespace CRMAPI.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentDescription { get; set; }
        public byte[] DocumentContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
