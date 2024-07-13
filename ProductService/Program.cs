using Microsoft.Extensions.Options;
using ProductService.Models;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ProductStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(ProductStoreDatabaseSettings)));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<ProductStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<ProductService.Services.ProductService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//app.Run();
app.Run("http://0.0.0.0:8080");
