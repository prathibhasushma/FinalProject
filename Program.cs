using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FinalProject1.Data;
using Microsoft.AspNetCore.Identity;
using FinalProject1.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FinalProject1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FinalProject1Context") ?? throw new InvalidOperationException("Connection string 'FinalProject1Context' not found.")));
builder.Services.AddDbContext<DataFinalProject1Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataFinalProject1ContextConnection")));
builder.Services.AddDefaultIdentity<FinalProject1User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataFinalProject1Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
