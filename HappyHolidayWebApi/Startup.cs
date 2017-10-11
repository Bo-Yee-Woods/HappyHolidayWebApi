using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HappyHolidayWebApi.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace HappyHolidayWebApi
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
            services.AddDbContext<DefaultDbContext>(options =>
            {
                var connectionString = "Server=tcp:sea-db-server.database.windows.net,1433;Initial Catalog=SEA-Free-32MB;Persist Security Info=False;User ID=baoyu;Password=Boyeewoods@1993;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                //var connectionString = "Data Source = localhost; Initial Catalog = HappyHoliday; Integrated Security = True;";
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseSqlServer(connectionString);

                // Register the entity sets needed by OpenIddict.                
                //options.UseOpenIddict();
            });

            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                setupAction.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
            });

            //services.AddAuthentication(option =>
            //{
            //    //option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    option.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //    //option.DefaultAuthenticateScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //    option.DefaultSignInScheme = "Cookies";
            //    option.DefaultAuthenticateScheme = "oidc";
            //})
            //.AddCookie()
            //.AddOpenIdConnect(options =>
            //{
            //    options.ClientId = "Happy_Holiday_Client";
            //    //options.ClientSecret = "123";           
                
            //    options.Authority = "https://localhost:44355/";

            //    options.RequireHttpsMetadata = true;
            //    options.SaveTokens = true;

            //    options.ResponseType = "code id_token";

            //    options.Scope.Add("openid");
            //    options.Scope.Add("profile");

            //});


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(applicationBuilder =>
                {
                    applicationBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected exception occurs.");
                    });
                });

                //app.UseOwin(async (context, next) => {
                //    context.
                //});

            }




            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
