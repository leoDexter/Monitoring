using DesafioStone.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Monitoring.ExternalAdapters;
using Monitoring.ExternalAdapters.Settings;
using MonitoringApi.dto;
using MonitoringApi.Filters;
using MonitoringApi.HostedServices;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace DesafioStone
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionHandling));
            });

            services.AddSingleton(typeof(IConfiguration), Configuration);

            #region Configure external api's adapters dependecy injections

            services.Configure<CityByCepApiSettings>(Configuration.GetSection("CityByCepApiSettings"));
            services.Configure<TemperatureApiSettings>(Configuration.GetSection("TemperatureApiSettings"));
            services.Configure<CityAutocompleteApiSettings>(Configuration.GetSection("CityAutocompleteApiSettings"));
            services.Configure<GeneralSettings>(Configuration.GetSection("GeneralSettings"));

            #endregion

            #region Configure monitoring service (servive updates temperatures)

            var defaultTemperatureApiSettings = new TemperatureApiSettings();
            Configuration.Bind("TemperatureApiSettings", defaultTemperatureApiSettings);

            var generalSettings = new GeneralSettings();
            Configuration.Bind("GeneralSettings", generalSettings);

            services.AddSingleton(typeof(IHostedService), new MonitoringService(Configuration, generalSettings, FacadeFactory<Temperature>.Create(defaultTemperatureApiSettings)));

            #endregion

            #region Configure Swagger
            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Desafio Stone Pagamentos, monitoramento de temperaturas",
                        Version = "v1",
                        Description = "Monitoramento de temperaturas das cidades cadastradas",
                        Contact = new Contact
                        {
                            Name = "Leonardo Fernandes",
                            Url = ""
                        }
                    });

                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Monitora a temperatura das cidades cadastradas, com o intervalo de 1 hora");
            });
        }
    }
}