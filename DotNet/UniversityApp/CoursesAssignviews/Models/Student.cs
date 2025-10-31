using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoursesAssignviews.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string RollNumber { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    
    public int? CourseId { get; set; }
    [JsonIgnore]
    public virtual Course? Course { get; set; }
}
