using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using RestfulWebApi.Data;
using RestfulWebApi.Interfaces;
using RestfulWebApi.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNLog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

// // Log when the application starts
// logger.Info("API started at {time}", DateTime.UtcNow);

// // Handle application shutdown event
// app.Lifetime.ApplicationStopping.Register(() =>
// {
//     logger.Info("API is shutting down at {time}", DateTime.UtcNow);
// });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials()
      //.WithOrigins("https://localhost:44351))
      .SetIsOriginAllowed(origin => true));

app.MapControllers();

app.Run();
