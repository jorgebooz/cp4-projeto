using ConsultoriaDevApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ─── MVC ───────────────────────────────────────────────────────────────────
builder.Services.AddControllersWithViews();

// ─── SQLite ────────────────────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ─── Autenticação por Cookie + Claims ──────────────────────────────────────
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AcessoNegado";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

// ─── Autorização por Role via Claims ───────────────────────────────────────
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SomenteAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("TechLiderOuAdmin", policy => policy.RequireRole("TechLider", "Admin"));
    options.AddPolicy("AreaInterna", policy => policy.RequireRole("TechLider", "Dev", "Admin"));
});

var app = builder.Build();

// ─── Pipeline ──────────────────────────────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // sempre ANTES de UseAuthorization
app.UseAuthorization();

// ─── Rotas ─────────────────────────────────────────────────────────────────
app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{action=Index}/{id?}",
    defaults: new { controller = "Admin" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();