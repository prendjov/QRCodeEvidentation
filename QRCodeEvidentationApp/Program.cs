using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QRCodeEvidentationApp.Data;
using QRCodeEvidentationApp.Models;
using QRCodeEvidentationApp.Repository.Implementation;
using QRCodeEvidentationApp.Repository.Interface;
using QRCodeEvidentationApp.Service.Implementation;
using QRCodeEvidentationApp.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(ILectureRepository), typeof(LectureRepository));
builder.Services.AddScoped(typeof(IProfessorRepository), typeof(ProfessorRepository));
builder.Services.AddScoped(typeof(ICourseRepository<CourseUserBaseEntity>), typeof(CourseRepository<CourseUserBaseEntity>));
builder.Services.AddScoped(typeof(IRoomRepository), typeof(RoomRepository));
builder.Services.AddScoped(typeof(ILectureCoursesRepository), typeof(LectureCoursesRepository));

builder.Services.AddTransient<ILectureService, LectureService>();
builder.Services.AddTransient<IProfessorService, ProfessorService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IRoomService, RoomService>();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
