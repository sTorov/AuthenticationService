using AuthenticationService.BLL.Middlewares.Extensions;
using AuthenticationService.BLL.Services;
using AuthenticationService.DAL.Repositories;
using AutoMapper;
using Microsoft.OpenApi.Models;
using ILogger = AuthenticationService.BLL.Services.ILogger;

namespace AuthenticationService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILogger, Logger>();                                       //Настройка DI контейнера
            services.AddSingleton<IUserRepository, UserRepository>();

            var mapperConfig = new MapperConfiguration(v =>                                 //Добавляем автомаппер
            {
                v.AddProfile(new MapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();                                                      //подключение контроллеров без представлений
            services.AddSwaggerGen(option =>                                                //изменение отображаемой информации в загаловке Swagger
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AuthenticationService",
                    Description = "AuthenticationService description"
                });
            });

            services.AddAuthentication(options => options.DefaultScheme = "Cookies")        //Добавляем способ аутентификации пользователей
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

            services.AddAuthorization(options =>                                            //Добавление политик для авторизации пользователей
            {
                options.AddPolicy("FromRussia", policy =>
                {
                    policy.RequireClaim("fromRussia", "True");                              //Использование Claim определённого типа с определённым значением
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();                                                           //Подключение Swagger
                app.UseSwaggerUI(c => {                                                     //user interface Swagger
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthenticationService v1");
                });
            }

            //app.UseMiddleware<LogMiddleware>();
            app.UseLogMiddleware();                                                         //Добавление middleware (фильтра)
            
            app.UseRouting();                                                               //Подключение маршрутизации
            app.UseAuthentication();                                                        //middleware Аутентификации
            app.UseAuthorization();                                                         //middleware Авторизации

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();                                                 //подключаем маршрутизацию на контроллеры без представлений
            });
        }
    }
}
