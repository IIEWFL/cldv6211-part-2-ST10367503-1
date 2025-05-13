using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VenueBookingSystem.Data;
using VenueBookingSystem.Models;
using VenueBookingSystem.Services; // BlobService

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the BlobService for Azure image uploads
builder.Services.AddSingleton<BlobService>();

var app = builder.Build();

// Seed data: Venues & Events
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    context.Database.EnsureCreated();

    if (!context.Venues.Any())
    {
        context.Venues.AddRange(
            new Venue
            {
                Name = "Main Hall",
                Location = "Midrand",
                Capacity = 200,
                ImageUrl = "/images/mainhall.jpg"
            },
            new Venue
            {
                Name = "Conference Room A",
                Location = "Braamfontein",
                Capacity = 100,
                ImageUrl = "/images/conferencerooma.jpg"
            },
            new Venue
            {
                Name = "Outdoor Stage",
                Location = "Cape Town",
                Capacity = 500,
                ImageUrl = "/images/outdoorstage.jpg"
            }
        );
        context.SaveChanges();
    }

    if (!context.Events.Any())
    {
        context.Events.AddRange(
            new Event
            {
                Name = "Tech Expo",
                Description = "Showcase of new technology",
                Location = "Main Hall",
                StartDate = DateTime.Today.AddDays(7),
                EndDate = DateTime.Today.AddDays(8)
            },
            new Event
            {
                Name = "Career Day",
                Description = "Networking event for students and employers",
                Location = "Conference Room A",
                StartDate = DateTime.Today.AddDays(14),
                EndDate = DateTime.Today.AddDays(14)
            }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Set the default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Event}/{action=Index}/{id?}");

app.Run();
