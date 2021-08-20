using ApiLogística.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.OpenApi.Models;

namespace ApiLogística
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
            string conexion = Configuration["Secrets:ConnectionSql"];
            //services.AddDbContext<DbeContext>(options => options.UseSqlite(conexion));
            services.AddDbContext<DbeContext>(options => options.UseSqlServer(conexion));

            services.AddSwaggerGen(c =>
            {
                var _contact = new OpenApiContact() { Name = "Alfonso Sarmiento Escorcia", Email = "manda1978@hotmail.com" };
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Api de Logistica",
                    Description = "Pruebas full stack tuya logistica",
                    Version = "v1",
                    Contact = _contact
                });
            });
            services.AddTransient<ILogisticaProvider, LogisticaProvider>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(string.Concat("/swagger/v1/swagger.json"), string.Concat("Pruebas full stack tuya", "API Logistica v1"));
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
