using DeskAspMvc.Data;
using DeskAspMvc.services;
using DeskAspMvc.services.AuthorizeServices;
using DeskAspMvc.services.Services2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<LocationService, LocationService>();
builder.Services.AddScoped<DeskService, DeskService>();
builder.Services.AddScoped<MyDateService, MyDateService>();
builder.Services.AddScoped<ReservationService,ReservationService>();
builder.Services.AddScoped<ReservationServiceAdapter,ReservationServiceAdapter>();


builder.Services.AddScoped<RoleManager<IdentityRole>, RoleManager<IdentityRole>>();
builder.Services.AddScoped<UserManager<IdentityUser>, UserManager<IdentityUser>>();

builder.Services.AddScoped<MyAuthorizeService,MyAuthorizeService>();


builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy =>
      policy.RequireRole("Admin"));



    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();


app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
