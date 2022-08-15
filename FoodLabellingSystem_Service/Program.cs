using FoodLabellingSystem_Service.Persistence;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using FoodLabellingSystem_Service.Services;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using FoodLabellingSystem_Service.Auth.AuthMVC.Persistence;
using FoodLabellingSystem_Service.Auth.AuthMVC.Services;
using FoodLabellingSystem_Service.Auth.AuthMVC;
using FoodLabellingSystem_Service.Auth.AuthMVC.Models;
using FoodLabellingSystem_Service.Auth.AuthMVC.Others;
using FoodLabellingSystem_Service.Auth.AuthAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICuisineTypeDAO, CuisineTypeDAO>();
builder.Services.AddScoped<IDishTypeDAO, DishTypeDAO>();
builder.Services.AddScoped<IFoodDAO, FoodDAO>();
builder.Services.AddScoped<IFoodIngredientDAO, FoodIngredientDAO>();
builder.Services.AddScoped<IIngredientDAO, IngredientDAO>();
builder.Services.AddScoped<IIngredientTypeDAO, IngredientTypeDAO>();
builder.Services.AddScoped<IUnitDAO, UnitDAO>();
builder.Services.AddScoped<IWarningDAO, WarningDAO>();
builder.Services.AddScoped<IUserDAO, UserDAO>();

builder.Services.AddScoped<ICuisineTypeService, CuisineTypeService>();
builder.Services.AddScoped<IDishTypeService, DishTypeService>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IFoodIngredientService, FoodIngredientService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IIngredientTypeService, IngredientTypeService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IWarningService, WarningService>();
builder.Services.AddScoped<IUserService, UserService>();

// login system mvc
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(config => {

    config.LoginPath = "/Users/Login";
    config.Cookie.Name = "FOODLABELLINGSYSTEM";
    config.AccessDeniedPath = "/AccessDenied";
    // reissues a new authentication when the half of the time past and the user is still interacting with the website
    config.SlidingExpiration = true;
    config.ExpireTimeSpan = TimeSpan.FromHours(2);
    // forces authentication over https
    config.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    // removes the chrome erros for coockie policies
    config.Cookie.SameSite = SameSiteMode.Strict;
    config.EventsType = typeof(AuthMVC<UserRepo>);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUser, User>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<AuthMVC<UserRepo>>();
builder.Services.AddSession();

// Auth API injection
builder.Services.AddAuthentication(options => options.DefaultScheme = "FoodLabellingSystem").AddScheme<AuthSchemeOptions, AuthHandler>("Delta", options => { });



// other services
builder.Services.AddScoped<IHashHelper, HashHelper>();
builder.Services.AddScoped<IEmailSender, EmailSender>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    Secure = CookieSecurePolicy.Always,
    // rejects cookies created by javascript
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always
};

app.UseCookiePolicy(cookiePolicyOptions);
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
