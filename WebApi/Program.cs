using UnhandledException.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<TestTaskRun>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

var _logger = app.Logger;
AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
  => _logger.LogError(e.ExceptionObject.ToString());

void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
  => _logger.LogError(e.Exception.ToString());
