using StudentManagementSystem.DTOs;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.Services;

public class CourseService(ICourseRepository repository) : ICourseService
{
    public async Task<IEnumerable<CourseDto>> GetAllAsync()
    {
        var courses = await repository.GetAllAsync();
        return courses.Select(ToDto);
    }

    public async Task<CourseDto?> GetByIdAsync(int id)
    {
        var course = await repository.GetByIdAsync(id);
        return course == null ? null : ToDto(course);
    }

    public async Task<CourseWithStudentsDto?> GetWithStudentsAsync(int id)
    {
        var course = await repository.GetByIdWithStudentsAsync(id);
        if (course == null)
        {
            return null;
        }

        return new CourseWithStudentsDto
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
    }

    public async Task<CourseDto> CreateAsync(CreateCourseDto dto)
    {
        var course = new Course
        {
            Name = dto.Name,
            Description = dto.Description
        };

        await repository.AddAsync(course);
        return ToDto(course);
    }

    public async Task<CourseDto> UpdateAsync(int id, CreateCourseDto dto)
    {
        var course = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"No course found with ID: {id}");

        course.Name = dto.Name;
        course.Description = dto.Description;

        await repository.UpdateAsync(course);
        return ToDto(course);
    }

    public async Task DeleteAsync(int id)
    {
        var course = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"No course found with ID: {id}");

        if (await repository.HasStudentsAsync(id))
        {
            throw new InvalidOperationException("Cannot delete a course that has students assigned.");
        }

        await repository.RemoveAsync(course);
    }

    private static CourseDto ToDto(Course course)
    {
        return new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description
        };
    }
}