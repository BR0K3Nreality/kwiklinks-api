using UrlShortener.Data;
using UrlShortener.Repositories;
using UrlShortener.Services;
using Npgsql;
using System.Data;

namespace UrlShortener
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var dbConfig = new DatabaseConfig
            {
                ConnectionString = Configuration.GetConnectionString("DefaultConnection")!
            };
            services.AddSingleton(dbConfig);

            services.AddScoped<IDbConnection>(sp =>
            {
                var conn = new NpgsqlConnection(dbConfig.ConnectionString);
                conn.Open();
                return conn;
            });

            services.AddScoped<IShortUrlRepository, ShortUrlRepository>();
            services.AddScoped<IShortUrlService, ShortUrlService>();

            // Configure CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost3000",
                    builder => builder
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddControllers();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UrlShortener v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // Enable CORS
            app.UseCors("AllowLocalhost3000");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
