using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data;

public class StudentDbContext : DbContext
{
    public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id).ValueGeneratedOnAdd();
            entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
            entity.Property(s => s.Email).IsRequired();
            entity.Property(s => s.CourseId).IsRequired();
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).ValueGeneratedOnAdd();
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Course)
            .WithMany(c => c.Students)
            .HasForeignKey(s => s.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Name = "Computer Science", Description = "Core programming and computer science fundamentals." },
            new Course { Id = 2, Name = "Data Science", Description = "Data analysis, statistics and machine learning." },
            new Course { Id = 3, Name = "Software Engineering", Description = "Software design, development and best practices." }
        );
    }
}