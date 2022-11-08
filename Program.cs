using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using AspNetCoreWebMinimalApi;
using AspNetCoreWebMinimalApi.Models;
using AspNetCoreWebMinimalApi.Repositories;
using AspNetCoreWebMinimalApi.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

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

builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseAuthentication();
app.UseAuthorization();

#region routes endpoints

app.MapPost("/auth/login", (UserLogin user, IUserService service, IValidator<UserLogin> validator) =>
    {
        var result = validator.Validate(user);
        if (!result.IsValid)
        {
            return Results.BadRequest(result.Errors.Select(d => d.ErrorMessage));
        }

        var info = service.Get(user);
        if (info is null) return Results.NotFound("User not found");


        var claims = new[]
        {
            new Claim(ClaimTypes.Name, info.Username),
            new Claim(ClaimTypes.Role, info.Role)
        };

        var token = new JwtSecurityToken
        (
            issuer: builder.Configuration["Jwt:Issuer"],
            audience: builder.Configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Results.Ok(tokenString);
    }
);

app.MapGet("/admin/secret",
        (ICurrentUserService current) =>
        {
            Console.WriteLine(current.Username);
            return Results.Ok("Shsss! 🤫 this is form admin");
        }
    )
    .RequireAuthorization(new CustomAuth(AppConstants.Role.Admin))
    ;

app.MapGet("/user/secret",
        (ICurrentUserService current) =>
        {
            Console.WriteLine(current.Username);
            return Results.Ok("Hello from user secret!");
        }
    )
    .RequireAuthorization(new CustomAuth(new[] { AppConstants.Role.User, AppConstants.Role.Admin }))
    ;

#endregion

app.Run();