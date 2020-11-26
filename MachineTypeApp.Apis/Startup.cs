using MachineTypeApp.Models.MachineTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MachineTypeApp.Apis
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MachineTypeApp.Apis", Version = "v1" });
            });

            #region CORS
            //[CORS] Angular, React 등의 SPA를 위한 CORS(Cross Origin Resource Sharing) 설정 1/2
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://localhost:3000"); // [!] Trailing Slash
                });
            });
            #endregion

            // MachineTypeApp 관련 의존성(종속성) 주입 관련 코드만 따로 모아서 관리 
            services.AddDependencyInjectionContainerForMachineTypeApp(Configuration.GetConnectionString("DefaultConnection"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MachineTypeApp.Apis v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            #region CORS
            //[CORS] Angular, React 등의 SPA를 위한 CORS(Cross Origin Resource Sharing) 설정 2/2
            app.UseCors(); // 반드시 UseRouting() 뒤에 와야 함  
            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
