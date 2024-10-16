using fakeface_be.Services;
using fakeface_be.Services.User;
using FakeFace_BE.DbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services
//builder.Services.AddSingleton(new DatabaseService(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository,UserRepository>();


// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var serverVersion = new MySqlServerVersion(new Version(9, 0, 1));
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, serverVersion);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
