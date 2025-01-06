namespace CRMAPI.Models
{
    public class CrmDataModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string CustomerType { get; set; }
    }
}
