using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories;

public class CourseRepository(StudentDbContext context) : ICourseRepository
{
    public async Task<List<Course>> GetAllAsync()
    {
        return await context.Courses
            .OrderBy(c => c.Id)
            .ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await context.Courses.FindAsync(id);
    }

    public async Task<Course?> GetByIdWithStudentsAsync(int id)
    {
        return await context.Courses
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> HasStudentsAsync(int courseId)
    {
        return await context.Students.AnyAsync(s => s.CourseId == courseId);
    }

    public async Task<Course> AddAsync(Course course)
    {
        context.Courses.Add(course);
        await context.SaveChangesAsync();
        return course;
    }

    public async Task<Course> UpdateAsync(Course course)
    {
        context.Courses.Update(course);
        await context.SaveChangesAsync();
        return course;
    }

    public async Task RemoveAsync(Course course)
    {
        context.Courses.Remove(course);
        await context.SaveChangesAsync();
    }
}