using FunBooksAndVideos.Api.Endpoints;
using FunBooksAndVideos.Api.Extensions;
using FunBooksAndVideos.Application.Interfaces;
using FunBooksAndVideos.Application.Processing;
using FunBooksAndVideos.Application.Services;
using FunBooksAndVideos.Domain.Interfaces;
using FunBooksAndVideos.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<OrderProcessor>();
builder.Host.AddSerilogConfiguration();

builder.Services.AddScoped<OrderProcessor>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IMembershipService, MembershipService>();
builder.Services.AddScoped<IShippingService, ShippingService>();

var app = builder.Build();

app.MapOrderEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();