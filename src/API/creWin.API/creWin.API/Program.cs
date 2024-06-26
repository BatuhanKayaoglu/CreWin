using creWin.API.Exceptions;
using creWin.API.Extensions;
using creWin.API.Extensions.JwtConf;
using creWin.API.LoggingConfigurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using CreWin.Infrastructure.Extensions;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureRegistration(builder.Configuration);
builder.Services.AddAPIRegistration(builder.Configuration);



//FOR SERILOG
//builder.Host.UseSerilog(new MainConfiguration().ConfigureLogger(builder, builder.Configuration)); // I make all the configurations in the Main Configuration section and just call it in Program.cs.
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    var mainConfiguration = new MainConfiguration();
    var logger = mainConfiguration.ConfigureLogger(builder, builder.Configuration);
    loggerConfiguration.WriteTo.Logger(logger);
});



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// If we are in a development environment, I send it this way because I want to see the details. However, when I upload the same application to PROD, I will only see the message part instead of all the details.
app.ConfigureExceptionHandling(app.Environment.IsDevelopment());

//FOR SERILOG
app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
