using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.DTOs;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories;

public class StudentRepository(StudentDbContext context) : IStudentRepository
{
    public async Task<(List<Student> Items, int TotalCount)> FindAsync(StudentQueryDto query)
    {
        IQueryable<Student> students = context.Students
            .Include(s => s.Course)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            students = students.Where(s =>
                s.Name.Contains(query.Search) || s.Email.Contains(query.Search));
        }

        if (query.CourseId.HasValue)
        {
            students = students.Where(s => s.CourseId == query.CourseId.Value);
        }

        if (query.Age.HasValue)
        {
            students = students.Where(s => s.Age == query.Age.Value);
        }

        var totalCount = await students.CountAsync();

        var items = await students
            .OrderBy(s => s.Id)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return (items, totalCount);
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