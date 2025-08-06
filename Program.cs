using HealthTipsPortal.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

// 🛢️ Configure EF Core with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=healthtips.db"));

// 🔐 Add Identity Services
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AppDbContext>();

// MVC + Razor Pages (needed for Identity)
builder.Services.AddRazorPages(); 
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🌐 Middleware Configuration
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // 👈 Added for login/register
app.UseAuthorization();

// 🧭 Route setup
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(); // 👈 Needed for Identity pages (Login/Register/etc.)

// 🌱 DB Seeding
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    DbInitializer.Seed(db);
}

app.Run();
