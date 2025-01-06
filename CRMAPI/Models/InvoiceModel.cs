namespace CRMAPI.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string InvoiceStatus { get; set; }
    }
}
