using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using POE_CLDV_6221_ST10224391;

namespace POE_CLDV_6221_ST10224391
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
