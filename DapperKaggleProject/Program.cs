using DapperKaggleProject;
using DapperKaggleProject.Data;
using DapperKaggleProject.Services;
using DapperKaggleProject.Services.DapperServices;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<NbadbContext>();



builder.Services.AddScoped<TeamsService>();


builder.Services.AddScoped<PerformanceComparisonService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
