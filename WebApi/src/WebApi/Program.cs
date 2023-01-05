using AutoMapper;
using BL;
using DAL;
using Infrastructure;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.RegisterDAL(builder.Configuration);
services.RegisterInfrastructure();
services.RegisterBL();
services.AddJwtAuthentication(builder.Configuration);
services.RegisterCors(builder.Configuration);
services.AddControllers();
services.AddAllAutoMappers();
// Start Registering and Initializing AutoMapper
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(WebApi.ServiceRegistration.CorsPolicyName);
app.UseAuthorization();

app.MapControllers();

app.Run();
