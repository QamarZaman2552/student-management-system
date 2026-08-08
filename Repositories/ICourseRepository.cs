using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories;

public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task<Course?> GetByIdWithStudentsAsync(int id);
    Task<bool> HasStudentsAsync(int courseId);
    Task<Course> AddAsync(Course course);
    Task<Course> UpdateAsync(Course course);
    Task RemoveAsync(Course course);
}