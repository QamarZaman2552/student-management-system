using StudentManagementSystem.DTOs;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.Services;

public class StudentService(IStudentRepository repository) : IStudentService
{
    public async Task<PagedResultDto<StudentDto>> GetAllAsync(StudentQueryDto query)
    {
        var (items, totalCount) = await repository.FindAsync(query);

        return new PagedResultDto<StudentDto>
        {
            Items = items.Select(ToDto).ToList(),
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize)
        };
    }

    public async Task<StudentDto?> GetByIdAsync(int id)
    {
        var student = await repository.GetByIdWithCourseAsync(id);
        return student == null ? null : ToDto(student);
    }

    public async Task<StudentDto> CreateAsync(CreateStudentDto dto)
    {
        await EnsureCourseExistsAsync(dto.CourseId);

        var student = new Student
        {
            Name = dto.Name,
            Email = dto.Email,
            Age = dto.Age,
            CourseId = dto.CourseId
        };

        await repository.AddAsync(student);
        return await GetByIdAsync(student.Id) ?? ToDto(student);
    }

    public async Task<StudentDto> UpdateAsync(int id, CreateStudentDto dto)
    {
        var student = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"No student found with ID: {id}");

        await EnsureCourseExistsAsync(dto.CourseId);

        student.Name = dto.Name;
        student.Email = dto.Email;
        student.Age = dto.Age;
        student.CourseId = dto.CourseId;

        await repository.UpdateAsync(student);
        return await GetByIdAsync(id) ?? ToDto(student);
    }

    public async Task DeleteAsync(int id)
    {
        var student = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"No student found with ID: {id}");

        await repository.RemoveAsync(student);
    }

    private async Task EnsureCourseExistsAsync(int courseId)
    {
        if (!await repository.CourseExistsAsync(courseId))
        {
            throw new InvalidOperationException($"No course found with ID: {courseId}. A valid CourseId is required.");
        }
    }

    private static StudentDto ToDto(Student student)
    {
        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age,
            CourseId = student.CourseId,
            CourseName = student.Course != null ? student.Course.Name : string.Empty
        };
    }
}