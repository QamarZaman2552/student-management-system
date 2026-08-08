using StudentManagementSystem.DTOs;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories;

public interface IStudentRepository
{
    Task<(List<Student> Items, int TotalCount)> FindAsync(StudentQueryDto query);
    Task<Student?> GetByIdAsync(int id);
    Task<Student?> GetByIdWithCourseAsync(int id);
    Task<bool> CourseExistsAsync(int courseId);
    Task<Student> AddAsync(Student student);
    Task<Student> UpdateAsync(Student student);
    Task RemoveAsync(Student student);
}