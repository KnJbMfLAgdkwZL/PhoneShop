using Application.Interfaces;
using Application.Services;
using Application.Services.RemoteAPI;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models.Entities.RemoteApi;

namespace PhoneShop
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
            //Database
            services.AddDbContext<MasterContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            //DataAccess
            services.AddScoped<IGeneric<Brand>, Generic<Brand>>();
            services.AddScoped<IBrands, RBrands>();
            
            //Application
            services.AddScoped<IPhoneSpecificationClient, PhoneSpecificationClient>();
            services.AddScoped<ISynchronizeDb, SynchronizeDb>();

            services.AddControllers();
            services.AddControllersWithViews();
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
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}