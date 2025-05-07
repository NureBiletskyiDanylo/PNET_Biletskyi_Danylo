using BookCatalog_API.Converters;
using BookCatalog_API.Extensions;
using BookCatalog_API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var tokenKey = builder.Configuration["TokenKey"] ?? throw new Exception("TokenKey not found");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.MSSqlServer(
    connectionString: builder.Configuration.GetConnectionString("Default"),
    sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true },
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
    .CreateLogger();

builder.Host.UseSerilog();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1");
});

app.UseReDoc(options =>
{
    options.SpecUrl("/openapi/v1.json");
});

app.MapScalarApiReference();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
