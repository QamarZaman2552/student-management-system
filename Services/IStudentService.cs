using StudentManagementSystem.DTOs;

namespace StudentManagementSystem.Services;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetAllAsync();
    Task<StudentDto?> GetByIdAsync(int id);
    Task<StudentDto> CreateAsync(CreateStudentDto dto);
    Task<StudentDto> UpdateAsync(int id, CreateStudentDto dto);
    Task DeleteAsync(int id);
}