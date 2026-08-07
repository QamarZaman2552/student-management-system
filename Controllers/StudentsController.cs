using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.DTOs;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(StudentDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudents()
    {
        var students = await context.Students
            .OrderBy(s => s.Id)
            .Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Age = s.Age,
                CourseId = s.CourseId,
                CourseName = s.Course != null ? s.Course.Name : string.Empty
            })
            .ToListAsync();

        return Ok(students);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentDto>> GetStudentById(int id)
    {
        Student? student = await context.Students
            .Include(s => s.Course)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null)
        {
            return NotFound(new { message = $"No student found with ID: {id}" });
        }

        return Ok(new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age,
            CourseId = student.CourseId,
            CourseName = student.Course != null ? student.Course.Name : string.Empty
        });
    }

    [HttpPost]
    public async Task<ActionResult<StudentDto>> AddStudent([FromBody] CreateStudentDto studentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool courseExists = await context.Courses.AnyAsync(c => c.Id == studentDto.CourseId);

        if (!courseExists)
        {
            return BadRequest(new { message = $"No course found with ID: {studentDto.CourseId}. A valid CourseId is required." });
        }

        var student = new Student
        {
            Name = studentDto.Name,
            Email = studentDto.Email,
            Age = studentDto.Age,
            CourseId = studentDto.CourseId
        };

        context.Students.Add(student);
        await context.SaveChangesAsync();

        var result = await context.Students
            .Include(s => s.Course)
            .FirstOrDefaultAsync(s => s.Id == student.Id);

        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, new StudentDto
        {
            Id = result!.Id,
            Name = result.Name,
            Email = result.Email,
            Age = result.Age,
            CourseId = result.CourseId,
            CourseName = result.Course != null ? result.Course.Name : string.Empty
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] CreateStudentDto studentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Student? student = await context.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound(new { message = $"No student found with ID: {id}" });
        }

        bool courseExists = await context.Courses.AnyAsync(c => c.Id == studentDto.CourseId);

        if (!courseExists)
        {
            return BadRequest(new { message = $"No course found with ID: {studentDto.CourseId}. A valid CourseId is required." });
        }

        student.Name = studentDto.Name;
        student.Email = studentDto.Email;
        student.Age = studentDto.Age;
        student.CourseId = studentDto.CourseId;

        await context.SaveChangesAsync();

        var result = await context.Students
            .Include(s => s.Course)
            .FirstOrDefaultAsync(s => s.Id == id);

        return Ok(new StudentDto
        {
            Id = result!.Id,
            Name = result.Name,
            Email = result.Email,
            Age = result.Age,
            CourseId = result.CourseId,
            CourseName = result.Course != null ? result.Course.Name : string.Empty
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        Student? student = await context.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound(new { message = $"No student found with ID: {id}" });
        }

        context.Students.Remove(student);
        await context.SaveChangesAsync();

        return NoContent();
    }
}