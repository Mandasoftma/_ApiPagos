using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPagos.Core.Interface;
using ApiPagos.Core.Service;
using ApiPagos.Infrastruture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ApiPagos
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
            services.AddTransient<ILogisticaService, LogisticaService>();
            services.AddTransient<IFacturasService, FacturasService>();
            services.AddHttpClient<IConsumerServices, ConsumerServices>();
            services.AddSwaggerGen(c =>
            {
                var _contact = new OpenApiContact() { Name = "Alfonso Sarmiento Escorcia", Email = "manda1978@hotmail.com" };
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Api de pago",
                    Description = "Pruebas full stack tuya",
                    Version = "v1",
                    Contact = _contact
                });
            });
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
                c.SwaggerEndpoint(string.Concat("/swagger/v1/swagger.json"), string.Concat("Pruebas full stack tuya", "API Pagos v1"));
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
