using CarWorkshop.Infrastructure.Persistance;
using CarWorkshop.Infrastructure.Seenders;
using CarWorkshop.Infrastructure.Extensions;
using CarWorkshop.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.ApplicationUser;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddSession();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAplication();


var app = builder.Build();

var scope = app.Services.CreateScope();

var seeder = scope.ServiceProvider.GetRequiredService<CarWorkshopSeeder>();

await seeder.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

public partial class Program { }