using StackExchange.Redis;
using LeaderboardService.DataAccess;
using LeaderboardService.Redis;
using LeaderboardService.Repositories;
using LeaderboardService.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// logging 

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .MinimumLevel.Information() 
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) 
        .MinimumLevel.Override("System", LogEventLevel.Warning) 
        .Enrich.FromLogContext()
        .WriteTo.Console(); 
});

// Add services to the container.

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILeaderboardService, LeaderboardService.Services.LeaderboardService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILeaderboardRepository, LeaderboardRepository>();

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
builder.Services.AddDbContext<LeaderboardContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddSingleton<RedisConnection>();
builder.Services.AddSingleton<IDatabase>(provider =>
{
    var redisConnection = provider.GetRequiredService<RedisConnection>();
    return redisConnection.GetDatabase();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (  app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();