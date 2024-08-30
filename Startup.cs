using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using POE_CLDV_6221_ST10224391.Services;

namespace POE_CLDV_6221_ST10224391
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            // Add Azure services
            var storageConnectionString = Configuration.GetConnectionString("AzureStorage");
            services.AddSingleton(new AzureTableStorageService(storageConnectionString));
            services.AddSingleton(new AzureBlobStorageService(storageConnectionString, "images"));
            services.AddSingleton(new AzureQueueStorageService(storageConnectionString, "processing-queue"));
            services.AddSingleton(new AzureFileStorageService(storageConnectionString, "files"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
