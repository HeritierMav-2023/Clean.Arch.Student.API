using Clean.Arch.Student.Application.IRepository;
using Clean.Arch.Student.Infrastructure.Config;
using Clean.Arch.Student.Infrastructure.Repository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Add Injection d'independance
builder.Services.Configure<ConnectionString>(builder.Configuration.GetSection("ConnectionString"));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Student.API", Version = "v2" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "Student.API v2"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
