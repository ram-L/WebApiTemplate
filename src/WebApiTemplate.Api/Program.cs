using Autofac.Core;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using WebApiTemplate.Api.Authorization;
using WebApiTemplate.Api.Configurations;
using WebApiTemplate.Api.Middlewares;
using WebApiTemplate.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure Autofac container
builder.Host.ConfigureAutofac(builder.Configuration);

// Configure Serilog for logging
builder.ConfigureLogging();

// Add OData support
builder.Services.AddControllers().AddOData(options => options.Select().Filter().OrderBy().Expand().SetMaxTop(250).Count());

// Add database context using dependency injection
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.ConfigureAppDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure JSON serialization options
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.ConfigureJsonOptions();
});

// Add Swagger configuration
builder.Services.AddSwaggerConfiguration(builder.Configuration);

// Add Jwt configuration
builder.Services.ConfigureJwtAuthentication(builder.Configuration);
builder.Services.Configure<JwtAuthOptions>(builder.Configuration.GetSection("Jwt"));

// Configure URL routing
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Apply database migrations
app.ApplyMigrations();

// Configure the HTTP request pipeline based on environment
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUIConfiguration(builder.Configuration);
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSerilogRequestLogging(); // Log HTTP requests

app.UseRouting();

app.UseAuthentication();
app.UseMiddleware<JwtAuthenticationMiddleware>();
app.UseAuthorization();

// Use your custom middleware if needed
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();
app.Run();

// Ensure any buffered events are sent at shutdown
Log.CloseAndFlush();
