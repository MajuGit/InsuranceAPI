# InsuranceAPI (InsuranceTPA) Web API

This project is a sample **Insurance TPA Web API** built with **.NET 8**, following Clean Architecture principles. It implements **JWT authentication**, layered architecture, and basic **Patient** and **Claim** management.

---

## 🚀 Features

- Layered architecture: Domain, Application, Infrastructure, API
- JWT-based authentication and role-based authorization
- Dependency Injection (DI) for services and repositories
- Exception handling middleware
- Entity Framework Core with SQLite database
- Swagger/OpenAPI documentation
- Unit tests with NUnit & Moq
- Async/await implemented for database operations
- Repository pattern implemented

---

## 📂 Project Structure

InsuranceAPI/
│
├── Domain/ # Entities
├── Application/ # Interfaces & business logic
├── Infrastructure/ # Repositories, DbContext, AuthService
├── API/ # Web API controllers & Program.cs
└── Tests/ # NUnit test project


---
--yaml
## 🛠 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- IDE: Visual Studio 2022 / VS Code
- Browser for testing Swagger UI

---

## 💻 Running the Application

1. Clone the repository:

git clone <repository-url>
cd InsuranceAPI

2.Restore dependencies:

dotnet restore

3.Build the solution:

dotnet build


4.Run the API:

dotnet run --project API/InsuranceAPI.API.csproj

5.Open your browser:

Default landing page:https://localhost:7155 (redirects to Swagger)

Swagger UI: https://localhost:7155/swagger/index.html

**Authentication

Login endpoint: POST /api/auth/login

Sample request:

{
  "username": "admin",
  "password": "admin123"
}

Response:

{
  "token": "<JWT token here>"
}

Use the JWT token in Authorization header for protected endpoints:

Authorization: Bearer <token>

**Running Tests

1.Navigate to the Tests project:

cd Tests

2.Run all tests using the .NET CLI:

dotnet test


--Tests are implemented using NUnit and Moq for mocking dependencies.

--Entity Framework Core InMemory provider is used for repository testing.


**Notes

The database used is SQLite (insurance.db in the project folder).

Ensure the Jwt settings in appsettings.json are correct:

"Jwt": {
  "Key": "ThisIsA32CharMinimumSuperSecretKey!!",
  "Issuer": "InsuranceTPA",
  "Audience": "InsuranceTPAUsers",
  "ExpireMinutes": 60
}