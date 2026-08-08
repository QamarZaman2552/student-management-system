using StudentManagementSystem.DTOs;

namespace StudentManagementSystem.Services;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetAllAsync();
    Task<CourseDto?> GetByIdAsync(int id);
    Task<CourseWithStudentsDto?> GetWithStudentsAsync(int id);
    Task<CourseDto> CreateAsync(CreateCourseDto dto);
    Task<CourseDto> UpdateAsync(int id, CreateCourseDto dto);
    Task DeleteAsync(int id);
}