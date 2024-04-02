using Library.Application.Contracts;
using Library.Application.Services;
using Library.Context;
using Library.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSession(op =>
            {
                op.IdleTimeout = TimeSpan.FromSeconds(55);

            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            /////////////////////////////////////////2//////////////////////////////////////////////////////
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(op =>
                {
                    // op.LoginPath = "/Account/Login";

                });
            ///////////////////////////////////////////////////////////////////////////////////////////////

           
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // عشان اشغل الرول
            builder.Services.AddIdentity<NewUser, IdentityRole>().AddEntityFrameworkStores<LibraryContext>();
            builder.Services.AddDbContext<LibraryContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("Db"));
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            ///////////////////////////////////////1////////////////////////////////////////////////////////
            app.UseAuthentication();
            ///////////////////////////////////////////////////////////////////////////////////////////////

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Book}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
