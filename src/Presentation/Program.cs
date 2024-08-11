using Microsoft.EntityFrameworkCore;
using Presentation.DbContext;
using Presentation.Extensions;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
builder.Services.AddRazorPages();
builder.Services.AddScoped<FaceService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddDbContext<RecognizerContext>(options =>
{
  options.UseNpgsql(configuration.GetConnectionString("Recognizer"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.ApplyMigrations();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapDownloadImage();

app.Run();
