# Student Management Web API

ASP.NET Core Web API (Day 4 - HisabDo Internship) for managing students using SQL Server with Entity Framework Core (Code First).

> **Also check the [Screenshot folder](screenshots) for all Swagger, Postman and SQL Server test screenshots.**

## How to Run

### 1. Configure the connection string

Edit `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=StudentManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 2. Run migrations

```bash
dotnet ef database update
```

This creates the `StudentManagementDB` database and `Students` table.

### 3. Run the API

```bash
dotnet run
```

Open `http://localhost:5067/swagger` to test with Swagger.

## API Endpoints

| Method   | Endpoint             | Description          |
|----------|----------------------|----------------------|
| GET      | /api/students        | Get all students     |
| GET      | /api/students/{id}   | Get student by ID    |
| POST     | /api/students        | Add a student        |
| PUT      | /api/students/{id}   | Update a student     |
| DELETE   | /api/students/{id}   | Delete a student     |

## Technologies

- ASP.NET Core (.NET 9)
- Entity Framework Core 9 (Code First)
- SQL Server 2025
- Swagger UI

## Project Structure

```
StudentManagementSystem/
├── Controllers/
│   └── StudentsController.cs
├── Data/
│   └── StudentDbContext.cs
├── DTOs/
│   ├── StudentDto.cs
│   └── CreateStudentDto.cs
├── Models/
│   └── Student.cs
├── Migrations/
└── Program.cs
```

## Sample

```json
{
  "id": 1,
  "name": "Qamar Zaman",
  "email": "qamar@hisabdo.com",
  "age": 21,
  "course": "Computer Science"
}
```

## Screenshots

### Day 3 - Swagger

![Day 3 Swagger 1](screenshots/Swagger_Day_3/Screenshot%202026-08-07%20111836.png)
![Day 3 Swagger 2](screenshots/Swagger_Day_3/Screenshot%202026-08-07%20112214.png)
![Day 3 Swagger 3](screenshots/Swagger_Day_3/Screenshot%202026-08-07%20112448.png)
![Day 3 Swagger 4](screenshots/Swagger_Day_3/Screenshot%202026-08-07%20112631.png)
![Day 3 Swagger 5](screenshots/Swagger_Day_3/Screenshot%202026-08-07%20112748.png)
![Day 3 Swagger 6](screenshots/Swagger_Day_3/Screenshot%202026-08-07%20113344.png)

### Day 3 - Postman

![Day 3 Postman 1](screenshots/Postman_Day_3/Screenshot%202026-08-07%20113513.png)
![Day 3 Postman 2](screenshots/Postman_Day_3/Screenshot%202026-08-07%20113552.png)
![Day 3 Postman 3](screenshots/Postman_Day_3/Screenshot%202026-08-07%20114221.png)
![Day 3 Postman 4](screenshots/Postman_Day_3/Screenshot%202026-08-07%20114624.png)
![Day 3 Postman 5](screenshots/Postman_Day_3/Screenshot%202026-08-07%20114757.png)
![Day 3 Postman 6](screenshots/Postman_Day_3/Screenshot%202026-08-07%20114915.png)

### Day 4 - Swagger

![Day 4 Swagger 1](screenshots/Swagger_Day_4_Task/Screenshot%202026-08-07%20125049.png)
![Day 4 Swagger 2](screenshots/Swagger_Day_4_Task/Screenshot%202026-08-07%20125225.png)
![Day 4 Swagger 3](screenshots/Swagger_Day_4_Task/Screenshot%202026-08-07%20125301.png)
![Day 4 Swagger 4](screenshots/Swagger_Day_4_Task/Screenshot%202026-08-07%20125428.png)
![Day 4 Swagger 5](screenshots/Swagger_Day_4_Task/Screenshot%202026-08-07%20125459.png)

### Day 4 - Postman

![Day 4 Postman 1](screenshots/Postman_Day_4_Task/Screenshot%202026-08-07%20125822.png)
![Day 4 Postman 2](screenshots/Postman_Day_4_Task/Screenshot%202026-08-07%20125858.png)
![Day 4 Postman 3](screenshots/Postman_Day_4_Task/Screenshot%202026-08-07%20125919.png)
![Day 4 Postman 4](screenshots/Postman_Day_4_Task/Screenshot%202026-08-07%20125959.png)
![Day 4 Postman 5](screenshots/Postman_Day_4_Task/Screenshot%202026-08-07%20130050.png)

### Day 4 - SQL Server

![Day 4 SQL Server](screenshots/SqlServer_Day_4/Screenshot%202026-08-07%20130341.png)