using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeddingPlanningReport;
using WeddingPlanningReport.Data;
using WeddingPlanningReport.Models;

var builder = WebApplication.CreateBuilder(args);
string WeddingPlanningName = "WeddingPlanningCors"; 
builder.Services.AddCors(options => {
    options.AddPolicy(WeddingPlanningName, policy => {
        policy.WithOrigins("*").WithMethods("*").WithHeaders("*");
    });
});
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
// MailService類別注入
builder.Services.AddScoped<MailService>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDbContext<WeddingPlanningContext>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("WeddingPlanning")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(WeddingPlanningName);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapControllers();
app.Run();
