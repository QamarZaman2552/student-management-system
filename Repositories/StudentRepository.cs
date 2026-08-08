using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories;

public class StudentRepository(StudentDbContext context) : IStudentRepository
{
    public async Task<List<Student>> GetAllAsync()
    {
        return await context.Students
            .Include(s => s.Course)
            .OrderBy(s => s.Id)
            .ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await context.Students.FindAsync(id);
    }

    public async Task<Student?> GetByIdWithCourseAsync(int id)
    {
        return await context.Students
            .Include(s => s.Course)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<bool> CourseExistsAsync(int courseId)
    {
        return await context.Courses.AnyAsync(c => c.Id == courseId);
    }

    public async Task<Student> AddAsync(Student student)
    {
        context.Students.Add(student);
        await context.SaveChangesAsync();
        return student;
    }

    public async Task<Student> UpdateAsync(Student student)
    {
        context.Students.Update(student);
        await context.SaveChangesAsync();
        return student;
    }

    public async Task RemoveAsync(Student student)
    {
        context.Students.Remove(student);
        await context.SaveChangesAsync();
    }
}