using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.DTOs;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(StudentDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses()
    {
        var courses = await context.Courses
            .OrderBy(c => c.Id)
            .Select(c => new CourseDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            })
            .ToListAsync();

        return Ok(courses);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CourseDto>> GetCourseById(int id)
    {
        Course? course = await context.Courses.FindAsync(id);

        if (course == null)
        {
            return NotFound(new { message = $"No course found with ID: {id}" });
        }

        return Ok(new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description
        });
    }

    [HttpGet("{id:int}/students")]
    public async Task<ActionResult<CourseWithStudentsDto>> GetCourseWithStudents(int id)
    {
        Course? course = await context.Courses
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
        {
            return NotFound(new { message = $"No course found with ID: {id}" });
        }

        var result = new CourseWithStudentsDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            Students = course.Students
                .OrderBy(s => s.Id)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Age = s.Age,
                    CourseId = s.CourseId,
                    CourseName = course.Name
                })
                .ToList()
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CourseDto>> AddCourse([FromBody] CreateCourseDto courseDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var course = new Course
        {
            Name = courseDto.Name,
            Description = courseDto.Description
        };

        context.Courses.Add(course);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] CreateCourseDto courseDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Course? course = await context.Courses.FindAsync(id);

        if (course == null)
        {
            return NotFound(new { message = $"No course found with ID: {id}" });
        }

        course.Name = courseDto.Name;
        course.Description = courseDto.Description;

        await context.SaveChangesAsync();

        return Ok(new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        Course? course = await context.Courses.FindAsync(id);

        if (course == null)
        {
            return NotFound(new { message = $"No course found with ID: {id}" });
        }

        bool hasStudents = await context.Students.AnyAsync(s => s.CourseId == id);

        if (hasStudents)
        {
            return BadRequest(new { message = "Cannot delete a course that has students assigned." });
        }

        context.Courses.Remove(course);
        await context.SaveChangesAsync();

        return NoContent();
    }
}