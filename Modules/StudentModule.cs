using AspNetCoreWebMinimalApi.Data;
using AspNetCoreWebMinimalApi.Models;
using Carter;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebMinimalApi.Modules;

public sealed class StudentModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var route = app.MapGroup("/students");
        route.MapGet("/", GetStudents);
        route.MapPost("/", CreateStudent);
        route.MapDelete("/{id:guid}", DeleteStudent);
        route.MapPut("/{id:guid}", UpdateStudent);
    }

    private static async Task<IResult> DeleteStudent(AppDbContext db, Guid id, CancellationToken ct)
    {
        if (await db.Set<Student>().FindAsync(id) is not { } doc) return Results.NotFound();

        db.Set<Student>().Remove(doc);
        await db.SaveChangesAsync(ct);
        return Results.Ok(doc);
    }

    private static async Task<IResult> UpdateStudent(AppDbContext db, Guid id, [FromBody] Student req,
        CancellationToken ct)
    {
        var student = (req with { Id = id }).Adapt<Student>();
        db.Attach(student);
        db.Entry(student).State = EntityState.Modified;
        await db.SaveChangesAsync(ct);
        return Results.NoContent();
    }


    private static async Task<IResult> CreateStudent(AppDbContext db, Student req, CancellationToken ct)
    {
        var student = req.Adapt<Student>();
        await db.Set<Student>().AddAsync(student, ct);
        await db.SaveChangesAsync(ct);
        return Results.Created($"/students/{student.Id}", student);
    }

    private static async Task<IResult> GetStudents(AppDbContext db, CancellationToken ct)
    {
        var students = await db.Set<Student>().ToListAsync(ct);
        return Results.Ok(students);
    }
}