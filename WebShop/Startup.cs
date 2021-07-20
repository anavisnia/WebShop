using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebShop.Authorization;
using WebShop.Data;
using WebShop.Entities;
using WebShop.Helpers;
using WebShop.Interfaces;
using WebShop.Repositories;
using WebShop.Services;
using BCryptNet = BCrypt.Net.BCrypt;

namespace WebShop
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(d => d.UseSqlServer(connectionString));
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddScoped<DiscountService>();

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            // configure DI for application services
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();

            // use always this interface/contract for this repository
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebShop", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext context)
        {
            createTestUser(context);

            app.UseRouting();

            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebShop v1"));
            }

            app.UseHttpsRedirection();

            //app.UseRouting();

            app.UseAuthorization();
            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        private void createTestUser(DataContext context)
        {
            // add hardcoded test user to db on startup
            var testUser = new User
            {
                FirstName = "Test",
                LastName = "User",
                Username = "test",
                PasswordHash = BCryptNet.HashPassword("test")
            };
            context.Users.Add(testUser);
            context.SaveChanges();
        }
    }
}
