using CRMAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<CrmDataModel> CrmData { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ActivityModel> Activities { get; set; }
        public DbSet<ReportModel> Reports { get; set; }
        public DbSet<InvoiceModel> Invoices { get; set; }
        public DbSet<PaymentModel> Payments { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<FeedbackModel> Feedbacks { get; set; }
        public DbSet<CampaignModel> Campaigns { get; set; }
        public DbSet<LeadModel> Leads { get; set; }
        public DbSet<TicketModel> Tickets { get; set; }
        public DbSet<InteractionModel> Interactions { get; set; }
        public DbSet<SalesPipelineModel> SalesPipelines { get; set; }
        public DbSet<DocumentModel> Documents { get; set; }
        public DbSet<ReminderModel> Reminders { get; set; }
    }
}
