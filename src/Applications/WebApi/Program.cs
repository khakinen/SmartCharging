using Data.Implementation;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Application.WebApi;
using SmartCharging.Domain.Implementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ExceptionHandlingMiddleware>();


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

builder.Services.AddDomainServices();
builder.Services.AddDataServices(builder.Configuration);

var app = builder.Build();

// Applying Database Migration!
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<SmartChargingDbContext>();
    
    dbContext.Database.Migrate();
}


app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();