using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MyCMS.Application.Repositories.Categuries.Interfaces;
using MyCMS.Application.Repositories.Categuries.Services;
using MyCMS.Application.Repositories.Comments.Interfaces;
using MyCMS.Application.Repositories.Comments.Services;
using MyCMS.Application.Repositories.Pages.Interfaces;
using MyCMS.Application.Repositories.Pages.Services;
using MyCMS.Application.Repositories.Users.Interfaces;
using MyCMS.Application.Repositories.Users.Services;
using MyCMS.DataLayer.AddAuditFieldInterceptors;
using MyCMS.DataLayer.Contexts;
using MyCMS.DataLayer.Models;

namespace MyCMS.WebSite
{
    public static class HostingExtension
    {
        #region Service
        public static WebApplication ConfigService(this WebApplicationBuilder applicationBuilder)
        {
            #region  AddServices
            applicationBuilder.Services.AddControllersWithViews();

            #region Authentication
            applicationBuilder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(option =>
                {
                    option.ExpireTimeSpan = TimeSpan.FromDays(30);
                    option.LoginPath = "/Login";
                    option.LogoutPath = "/Logout";
                });
            #endregion

            #region DiContainer
            var cnn = applicationBuilder.Configuration.GetConnectionString("MyConnection");
            applicationBuilder.Services.AddDbContext<MyCmsDbContext>(options => options.UseSqlServer(cnn));
            applicationBuilder.Services.AddScoped<IUserService, UserService>();
            applicationBuilder.Services.AddHttpContextAccessor(); // برای دسترسی به context
            applicationBuilder.Services.AddScoped<AddAuditFieldInterceptor>(); // خودش وابسته است به HttpContext
            applicationBuilder.Services.AddScoped<ICateguryService, CateguryService>();
            applicationBuilder.Services.AddScoped<ICommentService, CommentService>();
            applicationBuilder.Services.AddScoped<IPageService, PageService>();
            #endregion
            #endregion

            return applicationBuilder.Build();
        }
        #endregion

        #region Pipeline
        public static WebApplication ConfigPipeLine(this WebApplication application)
        {

            // Configure the HTTP request pipeline.
            if (!application.Environment.IsDevelopment())
            {
                application.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                application.UseHsts();
            }

            application.UseHttpsRedirection();
            application.UseRouting();
            application.UseAuthentication();
            application.UseAuthorization();

            application.MapStaticAssets();

            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
            application.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            return application;
        }
        #endregion
    }
}
