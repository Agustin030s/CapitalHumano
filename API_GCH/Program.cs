using Application;
using Application.Interfaces;
using Hangfire;
using Persistance;
using Shared;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationLayer();

var configuration = builder.Configuration;

builder.Services.AddPersistanceInfrastructure(configuration);
builder.Services.AddSharedInfrastructure(configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();

app.UseAuthorization();

app.MapControllers();

RecurringJob.AddOrUpdate<ISalaryService>(s => s.ProcessMonthlySalaries(), Cron.Monthly(2));

app.Run();
