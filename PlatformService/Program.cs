using Microsoft.EntityFrameworkCore;
using PlatformService.Application.AsyncDataServices;
using PlatformService.Application.BusinessLogic;
using PlatformService.Contracts.Application;
using PlatformService.Contracts.Repositories;
using PlatformService.Contracts.SyncDataServices.Http;
using PlatformService.Repository.DatabaseContext;
using PlatformService.Repository.Repositories;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsProduction())
    {
        var connectionString = builder.Configuration.GetConnectionString("PlatformsConnection");
        Console.WriteLine("Estamos en PRO usando sqlserver");
        Console.WriteLine($"ConnectionString: {connectionString}");
        options.UseSqlServer(connectionString, options =>
        options.MigrationsAssembly("PlatformService.Repository"));
    }
    else
    {
        Console.WriteLine("Estamos en PRE usando in memory");
        options.UseInMemoryDatabase("InMem");
    }
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddScoped<IPlatformManagement, PlatformManagement>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
var app = builder.Build();
var config = app.Services.GetRequiredService<IConfiguration>();
using var serviceScope = app.Services.CreateScope();
PrepDB.PopulateDb(serviceScope, builder.Environment.IsProduction());

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
