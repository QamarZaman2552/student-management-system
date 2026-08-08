using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DTOs;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CoursesController(ICourseService courseService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses()
    {
        return Ok(await courseService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CourseDto>> GetCourseById(int id)
    {
        var course = await courseService.GetByIdAsync(id);

        if (course == null)
        {
            return NotFound(new { message = $"No course found with ID: {id}" });
        }

        return Ok(course);
    }

    [HttpGet("{id:int}/students")]
    public async Task<ActionResult<CourseWithStudentsDto>> GetCourseWithStudents(int id)
    {
        var course = await courseService.GetWithStudentsAsync(id);

        if (course == null)
        {
            return NotFound(new { message = $"No course found with ID: {id}" });
        }

        return Ok(course);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CourseDto>> AddCourse([FromBody] CreateCourseDto courseDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var course = await courseService.CreateAsync(courseDto);

        return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] CreateCourseDto courseDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var course = await courseService.UpdateAsync(id, courseDto);

        return Ok(course);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        await courseService.DeleteAsync(id);

        return NoContent();
    }
}