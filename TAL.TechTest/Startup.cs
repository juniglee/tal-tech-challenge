using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TAL.TechTest.BLL.Interface;
using TAL.TechTest.BLL.Service;
using TAL.TechTest.DAL;
using TAL.TechTest.DAL.Interface;
using TAL.TechTest.DAL.Repository;

namespace TAL.TechTest
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddLogging();
            //services.AddSingleton<IConfigurationRoot>(Configuration);
            //services.AddSingleton<IMyService, MyService>();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration["DbConnectionString"]));
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IBlockoutRepository, BlockoutRepository>();
        }
    }
}
