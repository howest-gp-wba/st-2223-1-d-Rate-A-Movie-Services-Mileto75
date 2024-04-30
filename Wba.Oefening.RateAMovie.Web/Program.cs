using Microsoft.EntityFrameworkCore;
using Wba.Oefening.RateAMovie.Web.Data;
using Wba.Oefening.RateAMovie.Web.Services;
using Wba.Oefening.RateAMovie.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession();
// Register our own DbContext. It can now be injected and used by EF tooling.
builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("MovieDb")
    ));
//register own services
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IFormHelpersService, FormHelpersService>();
builder.Services.AddScoped<IAccountService, AccountService>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.Run();