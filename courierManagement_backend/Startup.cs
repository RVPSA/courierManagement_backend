using Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace courierManagement_backend
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
            LoadConfigurations();

            //Cors Policy
            services.AddCors(options => {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                     builder.WithOrigins(new[] { "http://localhost:5173" })
                     .AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowCredentials()
                ); 
               }
            );
            services.AddControllers();
        }

        private void LoadConfigurations() {
            AppSettings.DatabaseURL = Configuration.GetSection("ConnectionString:DatabaseURL").Value;
            AppSettings.SecretKey = Configuration.GetSection("OauthSettings:SecretKey").Value;
            AppSettings.TokenExpiration = Configuration.GetSection("OauthSettings:TokenExpiration").Value;

            AppSettings.CookieDomain = Configuration.GetSection("Cookies:Domain").Value;
            AppSettings.CookieExpire = Configuration.GetSection("Cookies:Expires").Value;
            AppSettings.CookiePath = Configuration.GetSection("Cookies:Path").Value;
            AppSettings.CookieName = Configuration.GetSection("Cookies:CookieName").Value;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //Enable Cors policy
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
