using System.Text;
using AspNetCoreWebMinimalApi;
using AspNetCoreWebMinimalApi.Data;
using AspNetCoreWebMinimalApi.Modules;
using AspNetCoreWebMinimalApi.Services;
using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlite($"Data Source={Path.Join(builder.Environment.ContentRootPath, "app.db")}"));

builder.Services.AddScoped<StudentModule>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AspNet Core Web Minimal Api",
        Version = "v1",
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateActor = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


builder.Services.AddAuthorization(options => { });

var app = builder.Build();

await app.ApplySqlLiteMigrations();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapCarter();

app.UseAuthentication();
app.UseAuthorization();


app.Run();