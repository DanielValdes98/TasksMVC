using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TareasMVC;
using Microsoft.AspNetCore.Mvc.Razor;
using TareasMVC.Servicios;

var builder = WebApplication.CreateBuilder(args);

var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

// Add services to the container.
builder.Services.AddControllersWithViews(opciones =>
{
    opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
}).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization(opciones => // Tecnica que permite utilizar un unico archivo de recursos para traducir las anotaciones de datos llamado RecursoCompartido
{
    opciones.DataAnnotationLocalizerProvider = (_, factoria) => factoria.Create(typeof(RecursoCompartido));
});

//builder.Services.AddControllersWithViews();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opciones => 
    opciones.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAuthentication().AddMicrosoftAccount(opciones =>
{
    opciones.ClientId = builder.Configuration["MicrosoftClientId"];
    opciones.ClientSecret = builder.Configuration["MicrosoftSecretId"];
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    opciones.SignIn.RequireConfirmedAccount = false; // No requiero una cuenta confirmada para que el usuario pueda loguearse
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders(); // Con este se activa Identity en la aplicacion

builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
    opciones =>
    {
        opciones.LoginPath = "/usuarios/login";
        opciones.AccessDeniedPath = "/usuarios/login"; // Cuando el acceso sea denegado
    });


// Internacionalización usando IStringLocalizer  
builder.Services.AddLocalization(opciones =>
{
    opciones.ResourcesPath = "Recursos";
});


var app = builder.Build();


// Configurar qué lenguajes va a soportar
//var culturasUISoportadas = new[] { "es", "en" }; // Anterior


// Vamos a localizar la peticion con el usario
app.UseRequestLocalization(opciones =>
{
    opciones.DefaultRequestCulture = new RequestCulture("es");  // ESTA ES LA CULTURA POR DEFECTO.
    opciones.SupportedUICultures = Constantes.CulturasUISoportadas
        .Select(cultura => new CultureInfo(cultura.Value)).ToList();
});

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
