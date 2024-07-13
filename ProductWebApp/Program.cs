using Microsoft.Extensions.Options;
using ProductWebApp.Models;
using ProductWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ProductServiceSettings>(
    builder.Configuration.GetSection("ProductService"));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<ProductServiceSettings>>().Value);

// Configure HttpClient with BaseAddress
builder.Services.AddHttpClient<ProductService>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<ProductServiceSettings>();
    client.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run("http://0.0.0.0:8080");
//app.Run();