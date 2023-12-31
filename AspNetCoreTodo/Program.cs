//using AspNetCoreTodo.Data;
using AspNetCoreTodo.Services;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using AspNetCoreTodo.Data;
using System;


var builder = WebApplication.CreateBuilder(args);

//public IConfiguration Configuration { get; }

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSingleton<ITodoItemService, FakeTodoItemService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));
builder.Services.AddScoped<ITodoItemService, TodoItemService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();