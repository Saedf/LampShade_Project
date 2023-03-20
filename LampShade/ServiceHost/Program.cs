using System.Text.Encodings.Web;
using System.Text.Unicode;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Infrastructure.Core;
using BlogManagement.Infrastructure.Core;
using CommentManagement.Infrastructure.Core;
using DiscountManagement.Infrastructure.Core;
using InventoryManagement.Infrastructure.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using ServiceHost;
using ShopManagement.Infrastructure.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
var connectionString = builder.Configuration.GetConnectionString("LampShadeDb");
ShopManagementBoostrapper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configure(builder.Services, connectionString);
InventoryManagementBootstrapper.Configure(builder.Services, connectionString);
BlogManagementBootstrapper.Configure(builder.Services, connectionString);
CommentManagementBootstrapper.Configure(builder.Services, connectionString);
AccountManagementBootstrapper.Configure(builder.Services, connectionString);


builder.Services.AddTransient<IFileUploader, FileUploader>();

builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });
builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminArea",builder => builder.RequireRole(new List<string> {Roles.Administrator,Roles.ContentUploader}));
        options.AddPolicy("Shop", builder => builder.RequireRole(new List<string> { Roles.Administrator }));
        options.AddPolicy("Account", builder => builder.RequireRole(new List<string> { Roles.Administrator }));
        options.AddPolicy("Discount", builder => builder.RequireRole(new List<string> { Roles.Administrator }));
        options.AddPolicy("Inventory", builder => builder.RequireRole(new List<string> { Roles.Administrator }));
    }
);
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Inventory", "Inventory");
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.Run();
