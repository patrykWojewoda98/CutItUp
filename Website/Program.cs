using CutItUp.Data.Context;
using EmailService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Website.Middlewares;
using Website.Services;
using Website.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CutItUpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext") ?? throw new InvalidOperationException("Connection string 'DbContext' not found.")));

// 1. Dodaj konfigurację EmailConfiguration jako singleton
var emailConfig = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();

builder.Services.AddSingleton(emailConfig);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".CutItUp.Session";
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<SessionValidationMiddleware>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();
app.UseSession();
app.UseMiddleware<SessionValidationMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    var persistentCookie = context.Request.Cookies["PersistentSession"];
    if (persistentCookie == "true")
    {
        context.Session.SetString("Persistent", "true");
    }

    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(),"..", "CutItUp.Data", "Data", "Video")), 
    RequestPath = "/Video"
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "..", "CutItUp.Data", "Data", "Images")),
    RequestPath = "/Images"
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "..", "CutItUp.Data")),
    RequestPath = "/Env"
});


app.UseRouting();

app.UseAuthorization();
DotNetEnv.Env.Load("../CutItUp.Data/.env");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
