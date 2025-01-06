# API Documentation

This API provides various endpoints for managing services and data operations. Below you will find a list of HTTP methods (GET, POST, PUT, DELETE) for each resource, along with brief descriptions of their functionality.

---

## Requirements

To run this project, the following requirements must be met:

- .NET 8 or later version.
- Microsoft SQL Server or a compatible database.

## Getting Started

Before you start the project, follow these steps:

###  Edit the `appsettings.json` File

You need to add the database connection string to the `appsettings.json` file used in the project.

Open the `appsettings.json` file and update the connection string as follows:

```json
{
  "ConnectionStrings": {
    "AuthConnString": "Server={SERVER_NAME};Database={DATABASE_NAME};Trusted_Connection=True;"
  },
}
```
- Replace {SERVER_NAME} with the name of your database server.
- Replace {DATABASE_NAME} with the name of your database. 


### Add Migration and Update Database
If you are running the project for the first time, you will need to update the database by running the Entity Framework Core migration commands.

Run the following commands in the terminal or command prompt to add the migration and update the database:

#### Add the migration:
```
dotnet ef migrations add InitialCreate
```

#### Update the database:
```
dotnet ef database update
```

### Running the Application
To run the project, follow these steps:

Open the terminal or command prompt in the project directory.

Run the following command to start the application:
```
dotnet run
```

# API Endpoints
## Activity Endpoints

### **GET** `/api/Activity`
Lists all activities.

### **POST** `/api/Activity`
Creates a new activity.

### **GET** `/api/Activity/{id}`
Retrieves a specific activity by its ID.

### **PUT** `/api/Activity/{id}`
Updates a specific activity by its ID.

### **DELETE** `/api/Activity/{id}`
Deletes a specific activity by its ID.

---

## Auth Endpoints

### **GET** `/api/Auth`
Retrieves general authentication information.

### **GET** `/api/Auth/{id}`
Retrieves a specific user by their ID.

### **POST** `/api/Auth/login`
Logs in a user.

### **POST** `/api/Auth/register`
Registers a new user.

### **PUT** `/api/Auth/update/{id}`
Updates user information.

### **DELETE** `/api/Auth/delete/{id}`
Deletes a user by their ID.

---

## Campaign Endpoints

### **GET** `/api/Campaign`
Lists all campaigns.

### **POST** `/api/Campaign`
Creates a new campaign.

### **GET** `/api/Campaign/{id}`
Retrieves a specific campaign by its ID.

### **PUT** `/api/Campaign/{id}`
Updates a specific campaign by its ID.

### **DELETE** `/api/Campaign/{id}`
Deletes a specific campaign by its ID.

---

## Crm Endpoints

### **GET** `/api/Crm`
Lists all CRM data.

### **POST** `/api/Crm`
Creates new CRM data.

### **GET** `/api/Crm/stats`
Retrieves statistics related to CRM.

### **GET** `/api/Crm/latest`
Retrieves the latest CRM data.

### **PUT** `/api/Crm/{id}`
Updates a specific CRM record by its ID.

### **DELETE** `/api/Crm/{id}`
Deletes a specific CRM record by its ID.

---

## Customer Endpoints

### **GET** `/api/Customer`
Lists all customers.

### **POST** `/api/Customer`
Creates a new customer.

### **GET** `/api/Customer/{id}`
Retrieves a specific customer by its ID.

### **PUT** `/api/Customer/{id}`
Updates a specific customer by its ID.

### **DELETE** `/api/Customer/{id}`
Deletes a specific customer by its ID.

---

## Document Endpoints

### **GET** `/api/Document`
Lists all documents.

### **POST** `/api/Document`
Creates a new document.

### **GET** `/api/Document/{id}`
Retrieves a specific document by its ID.

### **PUT** `/api/Document/{id}`
Updates a specific document by its ID.

### **DELETE** `/api/Document/{id}`
Deletes a specific document by its ID.

---

## Feedback Endpoints

### **GET** `/api/Feedback`
Lists all feedbacks.

### **POST** `/api/Feedback`
Creates a new feedback.

### **GET** `/api/Feedback/{id}`
Retrieves a specific feedback by its ID.

### **PUT** `/api/Feedback/{id}`
Updates a specific feedback by its ID.

### **DELETE** `/api/Feedback/{id}`
Deletes a specific feedback by its ID.

---

## Interaction Endpoints

### **GET** `/api/Interaction`
Lists all interactions.

### **POST** `/api/Interaction`
Creates a new interaction.

### **GET** `/api/Interaction/{id}`
Retrieves a specific interaction by its ID.

### **PUT** `/api/Interaction/{id}`
Updates a specific interaction by its ID.

### **DELETE** `/api/Interaction/{id}`
Deletes a specific interaction by its ID.

---

## Invoice Endpoints

### **GET** `/api/Invoice`
Lists all invoices.

### **POST** `/api/Invoice`
Creates a new invoice.

### **GET** `/api/Invoice/{id}`
Retrieves a specific invoice by its ID.

### **PUT** `/api/Invoice/{id}`
Updates a specific invoice by its ID.

### **DELETE** `/api/Invoice/{id}`
Deletes a specific invoice by its ID.

---

## Lead Endpoints

### **GET** `/api/Lead`
Lists all leads.

### **POST** `/api/Lead`
Creates a new lead.

### **GET** `/api/Lead/{id}`
Retrieves a specific lead by its ID.

### **PUT** `/api/Lead/{id}`
Updates a specific lead by its ID.

### **DELETE** `/api/Lead/{id}`
Deletes a specific lead by its ID.

---

## Order Endpoints

### **GET** `/api/Order`
Lists all orders.

### **POST** `/api/Order`
Creates a new order.

### **GET** `/api/Order/{id}`
Retrieves a specific order by its ID.

### **PUT** `/api/Order/{id}`
Updates a specific order by its ID.

### **DELETE** `/api/Order/{id}`
Deletes a specific order by its ID.

---

## Payment Endpoints

### **GET** `/api/Payment`
Lists all payments.

### **POST** `/api/Payment`
Creates a new payment.

### **GET** `/api/Payment/{id}`
Retrieves a specific payment by its ID.

### **PUT** `/api/Payment/{id}`
Updates a specific payment by its ID.

### **DELETE** `/api/Payment/{id}`
Deletes a specific payment by its ID.

---

## Product Endpoints

### **GET** `/api/Product`
Lists all products.

### **POST** `/api/Product`
Creates a new product.

### **GET** `/api/Product/{id}`
Retrieves a specific product by its ID.

### **PUT** `/api/Product/{id}`
Updates a specific product by its ID.

### **DELETE** `/api/Product/{id}`
Deletes a specific product by its ID.

---

## Reminder Endpoints

### **POST** `/api/Reminder`
Creates a new reminder.

### **GET** `/api/Reminder`
Lists all reminders.

### **GET** `/api/Reminder/{id}`
Retrieves a specific reminder by its ID.

### **DELETE** `/api/Reminder/{id}`
Deletes a specific reminder by its ID.

### **PUT** `/api/Reminder/{id}`
Updates a specific reminder by its ID.

### **GET** `/api/Reminder/user/{userId}`
Retrieves reminders for a specific user by their ID.

### **POST** `/api/Reminder/send-reminders`
Sends reminders to users.

---

## Report Endpoints

### **GET** `/api/Report`
Lists all reports.

### **POST** `/api/Report`
Creates a new report.

### **GET** `/api/Report/{id}`
Retrieves a specific report by its ID.

### **PUT** `/api/Report/{id}`
Updates a specific report by its ID.

### **DELETE** `/api/Report/{id}`
Deletes a specific report by its ID.

---

## SalesPipeline Endpoints

### **GET** `/api/SalesPipeline`
Lists all stages of the sales pipeline.

### **POST** `/api/SalesPipeline`
Creates a new sales pipeline stage.

### **GET** `/api/SalesPipeline/{id}`
Retrieves a specific sales pipeline stage by its ID.

### **PUT** `/api/SalesPipeline/{id}`
Updates a specific sales pipeline stage by its ID.

### **DELETE** `/api/SalesPipeline/{id}`
Deletes a specific sales pipeline stage by its ID.

---

## Task Endpoints

### **GET** `/api/Task`
Lists all tasks.

### **POST** `/api/Task`
Creates a new task.

### **GET** `/api/Task/{id}`
Retrieves a specific task by its ID.

### **PUT** `/api/Task/{id}`
Updates a specific task by its ID.

### **DELETE** `/api/Task/{id}`
Deletes a specific task by its ID.

---

## Ticket Endpoints

### **GET** `/api/Ticket`
Lists all tickets.

### **POST** `/api/Ticket`
Creates a new ticket.

### **GET** `/api/Ticket/{id}`
Retrieves a specific ticket by its ID.

### **PUT** `/api/Ticket/{id}`
Updates a specific ticket by its ID.

### **DELETE** `/api/Ticket/{id}`
Deletes a specific ticket by its ID.

---

## Data Models (Schemas)

The following data models define the structure of data used in the API:

- `ActivityModel`
- `CampaignModel`
- `CrmDataModel`
- `CustomerModel`
- `DocumentModel`
- `FeedbackModel`
- `InteractionModel`
- `InvoiceModel`
- `LeadModel`
- `LoginModel`
- `OrderModel`
- `PaymentModel`
- `ProductModel`
- `RegisterModel`
- `ReminderModel`
- `ReportModel`
- `SalesPipelineModel`
- `TaskModel`
- `TicketModel`

Each of these models represents the structure of data used in various API endpoints and is key for data interaction and storage.

---
