# Student Management Web API

ASP.NET Core Web API (Day 3 - HisabDo Internship) for managing students using an in-memory list.

## How to Run

```bash
cd StudentManagementSystem
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

## Tests

### Swagger

![GET all students](screenshots/Swagger/Screenshot%202026-08-07%20111836.png)

![Get by ID](screenshots/Swagger/Screenshot%202026-08-07%20112214.png)

![POST](screenshots/Swagger/Screenshot%202026-08-07%20112448.png)

![Validation error](screenshots/Swagger/Screenshot%202026-08-07%20112631.png)

![PUT](screenshots/Swagger/Screenshot%202026-08-07%20112748.png)

![DELETE](screenshots/Swagger/Screenshot%202026-08-07%20113344.png)

### Postman

![GET all](screenshots/Postman/Screenshot%202026-08-07%20113513.png)

![GET by ID 404](screenshots/Postman/Screenshot%202026-08-07%20113552.png)

![POST](screenshots/Postman/Screenshot%202026-08-07%20114221.png)

![PUT](screenshots/Postman/Screenshot%202026-08-07%20114624.png)

![DELETE](screenshots/Postman/Screenshot%202026-08-07%20114757.png)

![Validation error](screenshots/Postman/Screenshot%202026-08-07%20114915.png)