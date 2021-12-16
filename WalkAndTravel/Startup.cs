using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WalkAndTravel.ClassLibrary.Logging;
using WalkAndTravel.ClassLibrary.Middleware;
using WalkAndTravel.ClassLibrary.Repositories;
using WalkAndTravel.ClassLibrary.Services;
using WalkAndTravel.DataAccess;

namespace WalkAndTravel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                 .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                 .CreateLogger();

            services.AddDbContext<DataContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("Default")); options.AddInterceptors(new SQLQueriesInterceptor()); });
            services.AddControllersWithViews();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<RouteServices>().As<IRouteServices>().
                EnableInterfaceInterceptors().InterceptedBy(typeof(Logger)).InstancePerDependency();

            builder.RegisterType<UserServices>().As<IUserServices>().
                EnableInterfaceInterceptors().InterceptedBy(typeof(Logger)).InstancePerDependency();
            builder.RegisterType<RouteRepository>().As<IRouteRepository>().InstancePerDependency();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerDependency();
            builder.Register(x => Log.Logger).SingleInstance();
            builder.RegisterType<Logger>().SingleInstance();
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
                app.UseExceptionHandler("/Error");
            }

            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseMiddleware<LoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });


        }
    }
}
