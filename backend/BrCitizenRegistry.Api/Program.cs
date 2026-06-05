using BrCitizenRegistry.Application.Ports.In;
using BrCitizenRegistry.Application.Ports.Out;
using BrCitizenRegistry.Application.UseCases;
using BrCitizenRegistry.Infrastructure.Persistence;
using BrCitizenRegistry.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("A connection string 'DefaultConnection' não foi configurada.");
}

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions =>
        {
            mySqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
        }
    );
});

builder.Services.AddScoped<ICitizenRepository, MySqlCitizenRepository>();
builder.Services.AddScoped<ICitizenUseCase, CitizenUseCase>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        var frontendUrl = builder.Configuration["Frontend:Url"];

        if (!string.IsNullOrWhiteSpace(frontendUrl))
        {
            policy
                .WithOrigins(frontendUrl)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("FrontendPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();