using _01_Framework.Application;
using BlogManagement.Infrastructure.Core;
using CommentManagement.Infrastructure.Core;
using ServiceHost;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddHttpContextAccessor();
var connectionString = builder.Configuration.GetConnectionString("LampShadeDb");
BlogManagementBootstrapper.Configure(builder.Services, connectionString);
CommentManagementBootstrapper.Configure(builder.Services, connectionString);
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
// Configure the HTTP request pipeline.

var app = builder.Build();
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
app.MapControllers();
app.Run();
