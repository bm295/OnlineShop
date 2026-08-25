using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ShopDb") ?? "Data Source=shop.db"));
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddScoped<ShopDatabaseInitializer>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<ShopDatabaseInitializer>().Initialize();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
