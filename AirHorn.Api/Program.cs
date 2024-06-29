using AirHorn.Api.Classes;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(policy =>
  {
    /// CORS notes
    /// - Use a CORS tester: https://cors-test.codehappy.dev/
    /// - Be precise: Trailing forward slashes result in CORS block
    /// - IIS URL rewrite rules (redirects/rewrites) strip these headers & trigger CORS
    ///   - i.e. AllowAnyHeader() et al do not fix these problems
    /// - If IIS must intervene (URL rewrites, reverse proxies, etc.)
    ///   - Download IIS Cors module: https://www.iis.net/downloads/microsoft/iis-cors-module
    ///   - Configure: https://learn.microsoft.com/en-us/iis/extensions/cors-module/cors-module-configuration-reference#cors-configuration
    ///   - Keep Headers: https://www.carlosag.net/articles/enable-cors-access-control-allow-origin.cshtml
    ///   - Custom Headers: https://learn.microsoft.com/en-us/iis/extensions/url-rewrite-module/modifying-http-response-headers
    policy.WithOrigins("https://airhorn.tyleximus.com", "https://*.airhorn.tyleximus.com")
      .SetIsOriginAllowedToAllowWildcardSubdomains()
      .AllowAnyHeader() /// Allows header content-type
      .AllowAnyMethod() /// Allows method PUT
      .AllowCredentials();
  });
});

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
