
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Debug;
using SampleApp.Database.Tables;
using SampleApp.Web.Endpoints;

#if DEBUG
var debugLoggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });
#endif

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
{
  options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(300));
#if DEBUG
  options.UseLoggerFactory(debugLoggerFactory);
  options.EnableSensitiveDataLogging(true);
#endif
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
  options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
  options.KnownNetworks.Clear();
  options.KnownProxies.Clear();
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  builder.Services.AddDatabaseDeveloperPageExceptionFilter();

  app.UseDeveloperExceptionPage();

  app.UseMigrationsEndPoint();

  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapContactEndpoints();

app.Run();
