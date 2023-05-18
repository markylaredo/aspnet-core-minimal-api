namespace AspNetCoreWebMinimalApi.Models;

public sealed class Student
{

    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Level { get; init; }
    public string Section { get; init; }
    public string AcademicYear { get; init; }

}