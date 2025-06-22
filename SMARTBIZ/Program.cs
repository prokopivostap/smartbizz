using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartBiz.Application.DTO;
using SmartBiz.Application.Interfaces;
using SmartBiz.Domain.Models;
using SmartBiz.Infrastructure;
using SmartBiz.Infrastructure.DbSeed;
using SmartBiz.Infrastructure.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("SmartBiz.Infrastructure")));
builder.Services.AddScoped<ICustomerService, CustomerRepository>();
builder.Services.AddScoped<IInventoryService, InventoryRepository>();
builder.Services.AddScoped<IOrderService, OrderRepository>();
builder.Services.AddScoped<IFinancialRecordService, FinancialRecordRepository>();

builder.Services.AddIdentity<User, AppRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

var scope = app.Services.CreateScope();

DataSeeding.SeedRolesAndAdmin(
    scope.ServiceProvider.GetRequiredService<UserManager<User>>(),
    scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>()
    );

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=Index}/{id?}");

app.Run();
