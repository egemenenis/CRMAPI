namespace CRMAPI.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
    }
}
