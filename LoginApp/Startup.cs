using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoginApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // �N�b�L�[�|���V�[�̒ǉ�
            // Add a cookie policy
            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => !context.User.Identity.IsAuthenticated;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.Secure = CookieSecurePolicy.Always;
                options.HttpOnly = HttpOnlyPolicy.Always;
            });

            // �F�؋@�\�̒ǉ�
            // Add an authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.LoginPath = new PathString("/");
                    // �F�؂Ɏ��s�����ꍇ�ɕ\������r���[
                    // Set the view path for authorization error
                    options.AccessDeniedPath = new PathString("/Home/AuthorizationError");
                });

            services.AddRazorPages();
            // SQLServer�ւ̐ڑ�
            // Connect to SQLServer
            services.AddDbContext<LoginAppContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // �F�؋@�\�̒ǉ��̂��߃N�b�L�[�|���V�[�̒ǉ�
            app.UseCookiePolicy();

            app.UseRouting();

            // �ǉ�
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
