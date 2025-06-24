using Hos.Date;
using Hos.Models;
using Hos.Reposiories;
using Hos.Reposiories.IReposiories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Hos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddDbContext<ApplicationDbcontext>(options => options.UseSqlServer(builder.
                Configuration.GetConnectionString("DefultConnection")));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option => {
                option.SignIn.RequireConfirmedEmail = false;
                 
            })
                .AddEntityFrameworkStores<ApplicationDbcontext>()
                .AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login"; // Â‰« „”«— ’›Õ…  ”ÃÌ· «·œŒÊ· ⁄‰œﬂ
            });


            builder.Services.AddScoped<IDoctorRepositry , DoctorRepositry>();
            builder.Services.AddScoped<IAppointmentsRepositry , AppointmentsRepositry>();
            builder.Services.AddScoped<IAppliationUserReposity, AppliationUserReposity>();
            builder.Services.AddScoped<IProfilrRepositry , ProfilrRepositry>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Pation}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
