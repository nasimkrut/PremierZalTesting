using Application.Interfaces;
using Application.Services;
using Infrastructure;
using Infrastructure.BankApiClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    
builder.Services.AddScoped<IBankApiClient, BankApiClient>();
builder.Services.AddScoped<IImportTransactionsServices, ImportTransactionServices>();
builder.Services.AddScoped<IQueryTransactionServices, QueryTransactionServices>();
builder.Services.AddScoped<ICommandTransactionService, CommandTransactionServices>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.Run();