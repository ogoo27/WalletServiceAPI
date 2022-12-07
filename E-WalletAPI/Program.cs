using E_WalletAPI.Data;
using System;
using Microsoft.Extensions.DependencyInjection;
using E_WalletAPI.Services.Implimentations;
using E_WalletAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using E_WalletAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<E_walletDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EwalletConnectionString")));
//services
builder.Services.AddScoped<IAccountServices, AccountService>();
builder.Services.AddScoped<ITransactionServices, TransactionService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings")); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
