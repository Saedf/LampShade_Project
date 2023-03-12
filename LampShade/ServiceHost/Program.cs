using System.Text.Encodings.Web;
using System.Text.Unicode;
using _01_Framework.Application;
using BlogManagement.Infrastructure.Core;
using CommentManagement.Infrastructure.Core;
using DiscountManagement.Infrastructure.Core;
using InventoryManagement.Infrastructure.Core;
using ServiceHost;
using ShopManagement.Infrastructure.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("LampShadeDb");
ShopManagementBoostrapper.Configure(builder.Services,connectionString);
DiscountManagementBootstrapper.Configure(builder.Services, connectionString);
InventoryManagementBootstrapper.Configure(builder.Services, connectionString);
BlogManagementBootstrapper.Configure(builder.Services, connectionString);
CommentManagementBootstrapper.Configure(builder.Services,connectionString);

builder.Services.AddTransient<IFileUploader,FileUploader>();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin,UnicodeRanges.Arabic));
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
