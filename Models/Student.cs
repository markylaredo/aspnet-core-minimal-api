namespace AspNetCoreWebMinimalApi.Models;

public sealed class Student
{
    public Student(Guid id,
        string name,
        string level,
        string section,
        string academicYear)
    {
        Id = id;
        Name = name;
        Level = level;
        Section = section;
        AcademicYear = academicYear;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Level { get; init; }
    public string Section { get; init; }
    public string AcademicYear { get; init; }

}