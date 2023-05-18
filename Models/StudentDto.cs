namespace AspNetCoreWebMinimalApi.Models;

public sealed record StudentDto(Guid? Id, string Name, string Level, string Section, string AcademicYear);