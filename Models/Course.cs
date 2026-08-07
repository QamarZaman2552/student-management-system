using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models;

public class Course
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Course name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Course name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string Description { get; set; } = string.Empty;

    public List<Student> Students { get; set; } = new List<Student>();
}