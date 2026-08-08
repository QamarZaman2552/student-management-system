using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DTOs;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentsController(IStudentService studentService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResultDto<StudentDto>>> GetAllStudents([FromQuery] StudentQueryDto query)
    {
        return Ok(await studentService.GetAllAsync(query));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentDto>> GetStudentById(int id)
    {
        var student = await studentService.GetByIdAsync(id);

        if (student == null)
        {
            return NotFound(new { message = $"No student found with ID: {id}" });
        }

        return Ok(student);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<StudentDto>> AddStudent([FromBody] CreateStudentDto studentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var student = await studentService.CreateAsync(studentDto);

        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] CreateStudentDto studentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var student = await studentService.UpdateAsync(id, studentDto);

        return Ok(student);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        await studentService.DeleteAsync(id);

        return NoContent();
    }
}