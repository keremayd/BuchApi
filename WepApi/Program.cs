using Microsoft.EntityFrameworkCore;
using NLog;
using Repositories.Contracts;
using Repositories.EFCore;
using WebApi.Extensions;
using ILogger = NLog.ILogger;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(config =>
    {
        // İçerik pazarlığına açıyoruz
        config.RespectBrowserAcceptHeader = true;
        // İstemediğimiz tipte pazarlık isteği gelirse ona 406 Not Acceptable olacağın ayarlıyoruz
        config.ReturnHttpNotAcceptable = true;
    })
    .AddCustomCsvFormatter()
    // Artık xml formatında da çıkış veriyor olacağız
    .AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson();

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(Program));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();