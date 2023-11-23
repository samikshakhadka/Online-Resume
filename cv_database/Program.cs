using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using cv_database.Data;
using cv_database.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<cv_databaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cv_databaseContext") ?? throw new InvalidOperationException("Connection string 'cv_databaseContext' not found.")));

builder.Services.AddDbContext<DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase")));



builder.Services.AddCors(options =>
{
    options.AddPolicy("CROSPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});




// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IGenericRepos, GenericRepos>();
webBuilder.UseUrls("http://127.0.0.1:5001");

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
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
