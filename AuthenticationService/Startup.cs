using AuthenticationService.Repositories;
using AutoMapper;
using Microsoft.OpenApi.Models;

namespace AuthenticationService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILogger, Logger>();   //Настройка DI контейнера
            services.AddSingleton<IUserRepository, UserRepository>();

            var mapperConfig = new MapperConfiguration(v =>
            {
                v.AddProfile(new MapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();      //подключение контроллеров без представлений
            services.AddSwaggerGen(option =>    //изменение отображаемой информации в загаловке Swagger
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AuthenticationService",
                    Description = "AuthenticationService description"
                });
            });

            services.AddAuthentication(options => options.DefaultScheme = "Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = redirectContext =>
                        {
                            redirectContext.HttpContext.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthenticationService v1");
                });
            }
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();     //подключаем маршрутизацию на контроллеры
            });
        }
    }
}
