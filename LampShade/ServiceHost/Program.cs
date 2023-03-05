using _01_Framework.Application;
using DiscountManagement.Infrastructure.Core;
using InventoryManagement.Infrastructure.Core;
using ServiceHost;
using ShopManagement.Infrastructure.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("LampShadeDb");
ShopManagementBoostrapper.Configure(builder.Services,connectionString);
DiscountManagementBoostrapper.Configure(builder.Services, connectionString);
InventoryManagementBootstrapper.Configure(builder.Services, connectionString);

builder.Services.AddTransient<IFileUploader,FileUploader>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.Run();
