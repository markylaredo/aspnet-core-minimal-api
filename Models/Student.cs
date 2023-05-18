namespace AspNetCoreWebMinimalApi.Models;

public sealed record Student(Guid Id, string Name, string Level, string Section, string AcademicYear);