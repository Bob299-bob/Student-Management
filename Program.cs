
using Microsoft.EntityFrameworkCore;
using Student.Api.Data;
using Student.Api.Repositories;
using Student.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.Use(async (context, next) =>
{
    Console.WriteLine($"REQUEST ORIGIN: {context.Request.Headers.Origin}");
    await next();
});

app.UseCors("AngularPolicy");

app.Use(async (context, next) =>
{
    context.Response.OnStarting(() =>
    {
        Console.WriteLine(
            $"CORS RESPONSE: {context.Response.Headers.AccessControlAllowOrigin}"
        );

        return Task.CompletedTask;
    });

    await next();
});

app.UseAuthorization();
app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Urls.Add($"http://0.0.0.0:{port}");

app.Run();
