
using Microsoft.EntityFrameworkCore;
using Student.Api.Data;
using Student.Api.Repositories;
using Student.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Register controllers
builder.Services.AddControllers();

// Register PostgreSQL and EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Register Repository
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Register Service
builder.Services.AddScoped<IStudentService, StudentService>();

// Register CORS for Angular
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

// Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Enable CORS BEFORE authorization
app.UseCors("AngularPolicy");

app.UseAuthorization();

app.MapControllers();

// Render port configuration
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";

app.Urls.Add($"http://0.0.0.0:{port}");

app.Run();
