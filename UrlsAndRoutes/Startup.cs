using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore.Routing;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // map custom constraints to be usable inline
            services.Configure<RouteOptions>(options =>
                options.ConstraintMap.Add("weekday", typeof(WeekDayConstraint)));
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // default mvc routing
                endpoints.MapDefaultControllerRoute();

                // optional segment
                endpoints.MapControllerRoute(
                    name: "optional",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // custom segment
                endpoints.MapControllerRoute(
                    name: "MyRoute",
                    pattern: "{controller=Home}/{action=Index}/{id=DefaultId}");

                // static pattern
                endpoints.MapControllerRoute(
                    name: "shopSchema2",
                    pattern: "Shop/OldAction",
                    defaults: new { controller = "Home", action = "Index" });

                // static pattern with action
                endpoints.MapControllerRoute(
                    name: "shopSchema",
                    pattern: "Shop/{action}",
                    defaults: new { controller = "Home" });

                // routing with defaults
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}",
                    defaults: new { controller = "Home", action = "Index" });

                // routing with defaults
                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "Public/{controller=Home}/{action=Index}");

                // variable length *catchall will store all the url segments that comes after id
                endpoints.MapControllerRoute(
                    name: "MyRoute",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{*catchall}");

                // constrained type 1 specified inside the pattern
                endpoints.MapControllerRoute(
                    name: "MyRoute",
                    pattern: "{controller=Home}/{action=Index}/{id:int?}");

                // constrained type 2 specified outside the pattern
                endpoints.MapControllerRoute(
                    name: "MyRoute",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" },
                    constraints: new { id = new IntRouteConstraint() });

                // regular expression constraint 1 will only match controller that starts with H
                endpoints.MapControllerRoute(
                    name: "MyRoute",
                    pattern: "{controller:regex(^H.*)=Home}/{action=Index}/{id?}");

                // regular expression constraint 2 will only match action Index or About
                endpoints.MapControllerRoute(
                    name: "MyRoute",
                    pattern: "{controller:regex(^H.*)=Home}/{action:regex(^Index$|^About$)=Index}/{id?}");

                // type and value constraint will accept int with value between 10 and 20
                endpoints.MapControllerRoute(
                    name: "MyRoute",
                    pattern: "{controller=Home}/{action=Index}/{id:range(10,20)?}");

                // multiple constraints minimum of 6 alphabet characters
                endpoints.MapControllerRoute(
                    name: "MyRoute",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" },
                    constraints: new
                    {
                        id = new CompositeRouteConstraint(
                            new IRouteConstraint[] {
                                new AlphaRouteConstraint(),
                                new MinLengthRouteConstraint(6)
                            })
                    });

                // multiple constraints minimum of 6 alphabet characters (inline)
                endpoints.MapControllerRoute(
                    name: "MyRoute",
                    pattern: "{controller=Home}/{action=Index}/{id:alpha:minlength(6)?}");

                // custom constraint days of the week
                endpoints.MapControllerRoute(
                    name: "MyRoute",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" },
                    constraints: new { id = new WeekDayConstraint() });

                // custom constraint days of the week (inline)
                endpoints.MapControllerRoute(
                    name: "MyRoute",
                    pattern: "{controller=Home}/{action=Index}/{id:weekday?}");
            });
        }
    }
}
