using AutoMapper;
using AzureSearch;
using BL;
using DAL;
using Infrastructure;
using ScheduleTasks;
using WebApi;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Add services to the container.
var services = builder.Services;
services.RegisterDAL(builder.Configuration);
services.RegisterInfrastructure();
services.RegisterBL();
services.AddFileSettingOption(builder.Configuration);
services.AddEmailing(builder.Configuration);
services.AddJwtAuthentication(builder.Configuration);
services.RegisterCors(builder.Configuration);
services.AddControllers();
services.AddAllAutoMappers();
services.AddAzureSearch(builder.Configuration);
services.AddMemoryCache();
services.AddServiceFactory();
// Start Registering and Initializing AutoMapper
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.SetupScheduleTasks(builder.Configuration);
//services.InitScheduleTasks(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (true || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(WebApi.ServiceRegistration.CorsPolicyName);
app.UseAuthorization();

app.MapControllers();

app.Run();
