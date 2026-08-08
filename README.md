# Student & Course Management Web API

ASP.NET Core Web API (Day 6 - HisabDo Internship). Manages Students and Courses stored in SQL Server, with a **one-to-many relationship** (one Course has many Students) using Entity Framework Core (Code First), secured with **JWT Authentication** and role-based authorization (Admin/User).

Built with **Repository and Service patterns** for clean separation of concerns, plus a global **exception handling middleware** that returns proper HTTP status codes (400 / 404 / 500) as JSON.

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

Creates/updates the `StudentManagementDB` database (Students, Courses and Identity tables). 3 sample courses and an admin user are seeded automatically.

### 3. Run the API

```bash
dotnet run
```

Open `http://localhost:5067/swagger` to test with Swagger.

## Authentication (JWT)

### Test Accounts (seeded)

| Role  | Email              | Password   |
|-------|--------------------|------------|
| Admin | admin@hisabdo.com  | Admin@123  |
| User  | register new or use existing | - |

### Auth Endpoints

| Method | Endpoint            | Description                  |
|--------|---------------------|------------------------------|
| POST   | /api/auth/register  | Register a new user (User role) |
| POST   | /api/auth/login     | Login, returns a JWT token   |

### How to use the token

1. Call **login** and copy the `token` from the response.
2. Click the green **Authorize** button in Swagger and paste: `Bearer <your token>`
3. In Postman: Header → `Authorization: Bearer <your token>`

### Access Rules

| Endpoint                     | Role   |
|------------------------------|--------|
| GET /api/students, /api/courses | Any authenticated user |
| POST / PUT / DELETE (students & courses) | Admin only |
| /api/auth/register, /api/auth/login | Public (no token) |

Passwords are stored securely using ASP.NET Core Identity password hashing.

## Technologies

- ASP.NET Core (.NET 9)
- Entity Framework Core 9 (Code First)
- SQL Server 2025
- ASP.NET Core Identity + JWT Authentication
- Role-based Authorization (Admin / User)
- Swagger UI (with JWT support)

## Database Design

```
Courses (1) ----< (many) Students
```

- `Courses`: Id, Name, Description
- `Students`: Id, Name, Email, Age, CourseId (foreign key)
- Identity tables: AspNetUsers, AspNetRoles, AspNetUserRoles etc.

## API Endpoints

### Authentication

| Method   | Endpoint            | Description                       |
|----------|---------------------|-----------------------------------|
| POST     | /api/auth/register  | Register a new user               |
| POST     | /api/auth/login     | Login and get a JWT token         |

### Courses

| Method   | Endpoint                 | Description                     |
|----------|--------------------------|---------------------------------|
| GET      | /api/courses             | Get all courses                 |
| GET      | /api/courses/{id}        | Get course by ID                |
| GET      | /api/courses/{id}/students | Get a course with its students |
| POST     | /api/courses             | Add a course                    |
| PUT      | /api/courses/{id}        | Update a course                 |
| DELETE   | /api/courses/{id}        | Delete a course                 |

### Students

| Method   | Endpoint             | Description          |
|----------|----------------------|----------------------|
| GET      | /api/students        | Get all students     |
| GET      | /api/students/{id}   | Get student by ID    |
| POST     | /api/students        | Add a student        |
| PUT      | /api/students/{id}   | Update a student     |
| DELETE   | /api/students/{id}   | Delete a student     |

## Project Structure

```
StudentManagementSystem/
├── Controllers/
│   ├── AuthController.cs
│   ├── CoursesController.cs
│   └── StudentsController.cs
├── Data/
│   └── StudentDbContext.cs
├── DTOs/
│   ├── AuthResponseDto.cs
│   ├── CourseDto.cs
│   ├── CourseWithStudentsDto.cs
│   ├── CreateCourseDto.cs
│   ├── LoginDto.cs
│   ├── RegisterDto.cs
│   ├── StudentDto.cs
│   └── CreateStudentDto.cs
├── Models/
│   ├── AppUser.cs
│   ├── Course.cs
│   └── Student.cs
├── Repositories/
│   ├── IStudentRepository.cs
│   ├── StudentRepository.cs
│   ├── ICourseRepository.cs
│   └── CourseRepository.cs
├── Services/
│   ├── IAuthService.cs / AuthService.cs
│   ├── IStudentService.cs / StudentService.cs
│   └── ICourseService.cs / CourseService.cs
├── Middleware/
│   └── ExceptionHandlingMiddleware.cs
├── Migrations/
└── Program.cs
```

## Architecture

The API follows a layered pattern for clean separation of concerns:

- **Controllers** – handle HTTP requests/responses only (`[Authorize]`, validation).
- **Services** – business logic (mapping entities to DTOs, applying rules such as "a valid CourseId is required", "cannot delete a course that has students").
- **Repositories** – all Entity Framework Core data access (queries, CRUD).
- **Middleware** – global exception handling that converts failures into proper HTTP status codes:
  - `KeyNotFoundException` → 404 Not Found
  - `InvalidOperationException` → 400 Bad Request
  - any other exception → 500 Internal Server Error

## Sample JSON

### Course

```json
{
  "id": 1,
  "name": "Computer Science",
  "description": "Core programming and computer science fundamentals."
}
```

### Student

```json
{
  "id": 1,
  "name": "Qamar Zaman",
  "email": "qamar@hisabdo.com",
  "age": 21,
  "courseId": 1,
  "courseName": "Computer Science"
}
```

### Course with its Students

```json
{
  "id": 1,
  "name": "Computer Science",
  "description": "Core programming and computer science fundamentals.",
  "students": [
    {
      "id": 1,
      "name": "Qamar Zaman",
      "email": "qamar@hisabdo.com",
      "age": 21,
      "courseId": 1,
      "courseName": "Computer Science"
    }
  ]
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

### Day 5 - Swagger

![Day 5 Swagger 1](screenshots/Swagger_Day_5_Task/Screenshot%202026-08-07%20135224.png)
![Day 5 Swagger 2](screenshots/Swagger_Day_5_Task/Screenshot%202026-08-07%20135419.png)
![Day 5 Swagger 3](screenshots/Swagger_Day_5_Task/Screenshot%202026-08-07%20135454.png)
![Day 5 Swagger 4](screenshots/Swagger_Day_5_Task/Screenshot%202026-08-07%20135636.png)
![Day 5 Swagger 5](screenshots/Swagger_Day_5_Task/Screenshot%202026-08-07%20135700.png)
![Day 5 Swagger 6](screenshots/Swagger_Day_5_Task/Screenshot%202026-08-07%20135751.png)
![Day 5 Swagger 7](screenshots/Swagger_Day_5_Task/Screenshot%202026-08-07%20140056.png)
![Day 5 Swagger 8](screenshots/Swagger_Day_5_Task/Screenshot%202026-08-07%20140118.png)
![Day 5 Swagger 9](screenshots/Swagger_Day_5_Task/Screenshot%202026-08-07%20140232.png)
![Day 5 Swagger 10](screenshots/Swagger_Day_5_Task/Screenshot%202026-08-07%20140340.png)
![Day 5 Swagger 11](screenshots/Swagger_Day_5_Task/Screenshot%202026-08-07%20140405.png)

### Day 5 - Postman

![Day 5 Postman 1](screenshots/Postman_Day_5_Task/Screenshot%202026-08-07%20141435.png)
![Day 5 Postman 2](screenshots/Postman_Day_5_Task/Screenshot%202026-08-07%20143608.png)
![Day 5 Postman 3](screenshots/Postman_Day_5_Task/Screenshot%202026-08-07%20143651.png)
![Day 5 Postman 4](screenshots/Postman_Day_5_Task/Screenshot%202026-08-07%20143803.png)
![Day 5 Postman 5](screenshots/Postman_Day_5_Task/Screenshot%202026-08-07%20144114.png)
![Day 5 Postman 6](screenshots/Postman_Day_5_Task/Screenshot%202026-08-07%20144206.png)
![Day 5 Postman 7](screenshots/Postman_Day_5_Task/Screenshot%202026-08-07%20144318.png)
![Day 5 Postman 8](screenshots/Postman_Day_5_Task/Screenshot%202026-08-07%20144530.png)
![Day 5 Postman 9](screenshots/Postman_Day_5_Task/Screenshot%202026-08-07%20144551.png)
![Day 5 Postman 10](screenshots/Postman_Day_5_Task/Screenshot%202026-08-07%20145136.png)
![Day 5 Postman 11](screenshots/Postman_Day_5_Task/Screenshot%202026-08-07%20145216.png)

### Day 5 - SQL Server

![Day 5 SQL Server 1](screenshots/SqlServer_Day_5/Screenshot%202026-08-07%20145538.png)
![Day 5 SQL Server 2](screenshots/SqlServer_Day_5/Screenshot%202026-08-07%20145841.png)

### Day 6 - Swagger (JWT Authentication)

![Day 6 Swagger 1](screenshots/Swagger_Day_6_Task/Auth/Screenshot%202026-08-07%20200350.png)
![Day 6 Swagger 2](screenshots/Swagger_Day_6_Task/Auth/Screenshot%202026-08-07%20205802.png)
![Day 6 Swagger 3](screenshots/Swagger_Day_6_Task/Auth/Screenshot%202026-08-07%20210049.png)
![Day 6 Swagger 4](screenshots/Swagger_Day_6_Task/Auth/Screenshot%202026-08-07%20212220.png)
![Day 6 Swagger 5](screenshots/Swagger_Day_6_Task/Auth/Screenshot%202026-08-07%20212233.png)
![Day 6 Swagger 6](screenshots/Swagger_Day_6_Task/Auth/Screenshot%202026-08-07%20212302.png)
![Day 6 Swagger 7](screenshots/Swagger_Day_6_Task/Auth/Screenshot%202026-08-07%20212339.png)
![Day 6 Swagger 8](screenshots/Swagger_Day_6_Task/Auth/Screenshot%202026-08-07%20212415.png)