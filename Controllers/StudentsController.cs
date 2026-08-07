using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private static readonly List<Student> Students =
    [
        new Student { Id = 1, Name = "Qamar Zaman", Email = "qamar@hisabdo.com", Age = 21, Course = "Computer Science" },
        new Student { Id = 2, Name = "Ali Khan", Email = "ali@hisabdo.com", Age = 22, Course = "Data Science" }
    ];

    private static int nextId = 3;

    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetAllStudents()
    {
        return Ok(Students);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Student> GetStudentById(int id)
    {
        Student? student = Students.FirstOrDefault(s => s.Id == id);

        if (student == null)
        {
            return NotFound(new { message = $"No student found with ID: {id}" });
        }

        return Ok(student);
    }

    [HttpPost]
    public ActionResult<Student> AddStudent([FromBody] Student student)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        student.Id = nextId++;
        Students.Add(student);

        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateStudent(int id, [FromBody] Student student)
    {
        if (id != student.Id)
        {
            return BadRequest(new { message = "ID in URL does not match the student ID in the request body." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Student? existing = Students.FirstOrDefault(s => s.Id == id);

        if (existing == null)
        {
            return NotFound(new { message = $"No student found with ID: {id}" });
        }

        existing.Name = student.Name;
        existing.Email = student.Email;
        existing.Age = student.Age;
        existing.Course = student.Course;

        return Ok(existing);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteStudent(int id)
    {
        Student? student = Students.FirstOrDefault(s => s.Id == id);

        if (student == null)
        {
            return NotFound(new { message = $"No student found with ID: {id}" });
        }

        Students.Remove(student);

        return NoContent();
    }
}