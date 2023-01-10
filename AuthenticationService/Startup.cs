using Microsoft.OpenApi.Models;

namespace AuthenticationService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILogger, Logger>();   //Настройка DI контейнера

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
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();     //подключаем маршрутизацию на контроллеры
            });
        }
    }
}
