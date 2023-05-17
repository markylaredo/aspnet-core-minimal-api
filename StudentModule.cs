using AspNetCoreWebMinimalApi.Data;
using AspNetCoreWebMinimalApi.Models;
using Carter;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebMinimalApi;

public sealed class StudentModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var mapRoute = app.MapGroup("/students");
        mapRoute.MapGet("/", GetStudents);
        mapRoute.MapPost("/", CreateStudent);
    } 

    private static async Task<IResult> CreateStudent(AppDbContext db, Student student, CancellationToken ct)
    {
        await db.Set<Student>().AddAsync(student, ct);
        await db.SaveChangesAsync(ct);
        return Results.NoContent();
    }

    private static async Task<IResult> GetStudents(AppDbContext db, CancellationToken ct)
    {
        var students = await db.Set<Student>().ToListAsync(ct);
        return Results.Ok(students);
    }
}