using FoodLabellingSystem_Service.Persistence;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using FoodLabellingSystem_Service.Services;
using FoodLabellingSystem_Service.Controllers;
using FoodLabellingSystem_Service.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICuisineTypeDAO, CuisineTypeDAO>();
builder.Services.AddScoped<IDishTypeDAO, DishTypeDAO>();
builder.Services.AddScoped<IFoodDAO, FoodDAO>();
builder.Services.AddScoped<IFoodIngredientDAO, FoodIngredientDAO>();
builder.Services.AddScoped<IIngredientDAO, IngredientDAO>();
builder.Services.AddScoped<IIngredientTypeDAO, IngredientTypeDAO>();
builder.Services.AddScoped<IUnitDAO, UnitDAO>();
builder.Services.AddScoped<IWarningDAO, WarningDAO>();

builder.Services.AddScoped<ICuisineTypeService, CuisineTypeService>();
builder.Services.AddScoped<IDishTypeService, DishTypeService>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IFoodIngredientService, FoodIngredientService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IIngredientTypeService, IngredientTypeService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IWarningService, WarningService>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
