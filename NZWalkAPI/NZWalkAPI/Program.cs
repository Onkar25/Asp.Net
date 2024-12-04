﻿using NZWalkAPI.Data;
using Microsoft.EntityFrameworkCore;
using NZWalkAPI.Repository;
using AutoMapper;
using NZWalkAPI.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NZWalkDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

builder.Services.AddScoped<IRegionRepository, SqlRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SqlWalkRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
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
