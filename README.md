
# eCommerceApp API

This project is an **E-commerce Web API** built using **ASP.NET Core Web API**, designed to manage products, categories, orders, and payments. The API provides user authentication, role-based access control, and Stripe integration for secure payments.

## Table of Contents
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [Setup and Installation](#setup-and-installation)


---

## Features
- **User Authentication**: Users can register, log in, and access different features based on their roles (admin, user).  
- **Role-based Access Control**: Admins can manage products, categories, and orders. Users can browse products, place orders, and make payments.  
- **Product Management**: Admins can create, update, and delete products, including adding descriptions, prices, and assigning categories.  
- **Category Management**: Admins can create, update, and delete product categories.  
- **Order Management**: Users can place orders, view order details, and track order status.  
- **Stripe Payment Integration**: Users can securely pay for orders via Stripe Checkout.  
- **DTO Mapping**: Clean separation between domain models and data transfer objects using Mapster/AutoMapper.

---

## Technologies Used
- **ASP.NET Core Web API**: Backend framework for building the API.  
- **Entity Framework Core**: ORM for database interactions.  
- **SQL Server**: Database to store products, categories, orders, and user information.  
- **C#**: Programming language used for server-side logic.  
- **Stripe API**: Payment processing.  
- **Swagger / OpenAPI**: API documentation and testing.

---

## Project Structure
The project follows a **layered architecture** to keep the code clean, maintainable, and scalable:


**Explanation of layers:**

- **Application Layer**: Contains business logic, services, DTOs (Data Transfer Objects), and interfaces for clean separation.  
- **Domain Layer**: Contains core entities/models and enums representing business objects.  
- **Infrastructure Layer**: Handles data access (repositories), database interaction via Entity Framework, and Stripe payment integration.  
- **Host Layer**: API controllers, Swagger configuration, and entry point (`Program.cs`).  

---

## Setup and Installation
1. **Clone the repository**:
   ```bash
   git clone https://github.com/Adel627/eCommerceApp.git

2. **Navigate to the project folder**:
   ```bash
   cd eCommerceApp

3. **Install dependencies: Make sure you have the required .NET SDK installed. Run the following command to restore dependencies:**:
   ```bash
   dotnet restore

4.  **Database Setup: Update the appsettings.json file with your database connection string. Then, run migrations to set up the database schema:**
    ```bash
    dotnet ef database update

5.  **Run the API:**
    ```bash
    dotnet run

6.  **Access Swagger UI:**
    ```bash
    https://localhost:{port}/swagger

7.  **Testing Protected Routes:**
   
     Use Postman or Swagger UI.
     Include JWT token in the Authorization header for endpoints requiring authentication:
    ```bash
    Authorization: Bearer {your_token_here}           
