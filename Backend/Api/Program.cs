using DGIIAPP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using dotenv.net;
using DGIIAPP.Application.Interfaces.Services;
using DGIIAPP.Infrastructure.Services;
using DGIIAPP.API.Middlewares;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:3000");
                      });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    DotEnv.Load();

    string? server = Environment.GetEnvironmentVariable("DB_SERVER");
    string? database = Environment.GetEnvironmentVariable("DB_NAME");
    string? user = Environment.GetEnvironmentVariable("DB_USER");
    string? password = Environment.GetEnvironmentVariable("DB_PASSWORD");
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    connectionString = connectionString?.ToString()
                                        .Replace("{DB_SERVER}", server)
                                        .Replace("{DB_NAME}", database)
                                        .Replace("{DB_USER}", user)
                                        .Replace("{DB_PASSWORD}", password);
    options.UseSqlServer(connectionString);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// App services registration
builder.Services.AddScoped<IContribuyenteService, ContribuyenteService>();
builder.Services.AddScoped<IComprobanteFiscalService, ComprobanteFiscalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
builder.Services.AddLogging();
app.UseCors(MyAllowSpecificOrigins);
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
