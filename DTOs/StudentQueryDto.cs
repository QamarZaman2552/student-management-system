using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.DTOs;

public class StudentQueryDto
{
    [StringLength(100, ErrorMessage = "Search text must not exceed 100 characters.")]
    public string? Search { get; set; }

    public int? CourseId { get; set; }

    [Range(1, 150, ErrorMessage = "Age must be between 1 and 150.")]
    public int? Age { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be at least 1.")]
    public int PageNumber { get; set; } = 1;

    [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
    public int PageSize { get; set; } = 10;
}