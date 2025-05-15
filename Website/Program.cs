using CutItUp.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Website.Middlewares;
using Website.Services;
using Website.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CutItUpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext") ?? throw new InvalidOperationException("Connection string 'DbContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    //zastanwić sie jak to zrobic żeby nas zapamietywało tylko po zaznaczeniu opcji zapamiętaj mnie
    //options.Cookie.MaxAge = TimeSpan.FromMinutes(3);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".CutItUp.Session";
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<SessionValidationMiddleware>();//dopiero tutaj!!!!

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
