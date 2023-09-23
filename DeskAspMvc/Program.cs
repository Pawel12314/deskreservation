using DeskAspMvc.Data;
using DeskAspMvc.services;
using DeskAspMvc.services.Services2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

/*
 * services.AddPersistenceServices();

    services.AddControllers();
 */
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("Open",
        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});*/


//var container = new UnityContainer();
//container.RegisterType<IUserMasterRepository, UserMasterRepository>();
//DependencyResolver.SetResolver(new UnityDependencyResolver(container));
builder.Services.AddScoped<LocationService2, LocationService2>();
builder.Services.AddScoped<DeskService2, DeskService2>();


builder.Services.AddScoped<RoleManager<IdentityRole>, RoleManager<IdentityRole>>();
builder.Services.AddScoped<UserManager<IdentityUser>, UserManager<IdentityUser>>();

builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy =>
      policy.RequireRole("Admin"));



    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

//builder.Services.AddRazorPages()
var app = builder.Build();


app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

/*app.UseCors("Open");
*/app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

var scope = app.Services.CreateScope();
//scope.ServiceProvider
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

IdentityResult roleResult;

// Adding Admin Role
var roleCheck = await roleManager.RoleExistsAsync("Admin");
if (!roleCheck)
{
    //Create the roles and seed them to the database 
    roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
}

//UserManager.GetEmailAsync()

// Assign Admin role to newly registered user
IdentityUser user = await userManager.FindByEmailAsync("testemail@gmail.com");
if (user != null)
{
    var User = new IdentityUser();
    
    await userManager.AddToRoleAsync(user, "Admin");
}


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
