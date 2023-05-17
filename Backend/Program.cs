using Backend.Data;
using Microsoft.EntityFrameworkCore;
using dotenv.net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    DotEnv.Load();

    string? server = Environment.GetEnvironmentVariable("DB_SERVER");
    string? database = Environment.GetEnvironmentVariable("DB_NAME");
    // string? user = Environment.GetEnvironmentVariable("DB_USER");
    // string? password = Environment.GetEnvironmentVariable("DB_PASSWORD");
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    connectionString = connectionString.Replace("{DB_SERVER}", server)
                                        .Replace("{DB_NAME}", database)
                                        /*.Replace("{DB_USER}", user)
                                        .Replace("{DB_PASSWORD}", password)*/;
    options.UseSqlServer(connectionString);
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();
