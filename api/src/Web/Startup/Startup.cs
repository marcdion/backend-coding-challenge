using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SuggestionApi.Domain.Helpers.Geo;
using SuggestionApi.Domain.Helpers.Scoring;
using SuggestionApi.Domain.Helpers.Scoring.Parameters.EditDistance;
using SuggestionApi.Domain.Helpers.Scoring.Parameters.PopularityScore;
using SuggestionApi.Domain.Helpers.Scoring.Parameters.PopulationScore;
using SuggestionApi.Domain.Helpers.Seed;
using SuggestionApi.Domain.Models.DataStructure;
using SuggestionApi.Domain.Models.ScoringWeights;

namespace SuggestionApi.Web.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string CORS_POLICY = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddApiVersioning(o =>
            {
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(2, 0);
            });            
            
            services.AddCors(options =>
            {
                options.AddPolicy(CORS_POLICY, builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            
            //Adding services
            services.AddSingleton<ISeedDomainService, SeedDomainService>();
            services.AddSingleton<IScoringDomainService, ScoringDomainService>();
            services.AddSingleton<IGeoDomainService, GeoDomainService>();
            services.AddSingleton<SharedTrie>();
            services.AddSingleton<SharedScoringWeight>();
            
            services.AddSingleton<ILogarithmicEditDistance, LogarithmicEditDistance>();
            services.AddSingleton<ILogarithmicPopularityScore, LogarithmicPopularityScore>();
            services.AddSingleton<ILogarithmicPopulationScore, LogarithmicPopulationScore>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("1.0", new OpenApiInfo
                {
                    Version = "Version 1.0",
                    Title = "Suggestions API documentation",
                    Description = "This API provides auto complete suggestions for location search",
                    Contact = new OpenApiContact
                    {
                        Name = "Marc-Antoine Dion",
                        Email = "marc-antoine.dion@gmddeveloppement.com"
                    }
                });
                
                c.SwaggerDoc("2.0", new OpenApiInfo
                {
                    Version = "Version 2.0",
                    Title = "Suggestions API documentation",
                    Description = "This API provides auto complete suggestions for location search",
                    Contact = new OpenApiContact
                    {
                        Name = "Marc-Antoine Dion",
                        Email = "marc-antoine.dion@gmddeveloppement.com"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(CORS_POLICY);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/1.0/swagger.json", "Suggestion API - Version 1.0");
                c.SwaggerEndpoint("/swagger/2.0/swagger.json", "Suggestion API - Version 2.0");
            });
        }
    }
}
