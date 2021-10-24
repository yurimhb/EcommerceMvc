using CasaDoCodigo.Repositories;
using ECommerceMVC.Areas.Catalogo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace CasaDoCodigo
{

    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;

        public Startup(ILoggerFactory loggerFactory,
            IConfiguration configuration)
        {
            _loggerFactory = loggerFactory;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession();

            string connectionString = Configuration.GetConnectionString("Default");
            string connectionStringCatalogo = Configuration.GetConnectionString("Catalogo");

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString)
            );

            services.AddDbContext<CatalogoDbContext>(options =>
                options.UseSqlServer(connectionStringCatalogo)
            );

            services.AddTransient<IDataService, DataService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IHttpHelper, HttpHelper>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<ICadastroRepository, CadastroRepository>();
            services.AddTransient<IRelatorioHelper, RelatorioHelper>();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IServiceProvider serviceProvider)
        {
            _loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                    name: "AreaCatalago",
                    areaName: "Catalogo",
                    template: "Catalogo/{controller=Home}/{action=Index}/{pesquisa?}"
                    );
                routes.MapAreaRoute(
                    name: "AreaCarrinho",
                    areaName: "Carrinho",
                    template: "Carrinho/{controller=Home}/{action=Index}/{pesquisa?}"
                    );
                routes.MapAreaRoute(
                   name: "AreaCadastro",
                   areaName: "Cadastro",
                   template: "Cadastro/{controller=Home}/{action=Index}/{pesquisa?}"
                   );
                routes.MapAreaRoute(
                   name: "AreaPedido",
                   areaName: "Pedido",
                   template: "Pedido/{controller=Home}/{action=Index}/{pesquisa?}"
                   );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{codigo?}");
            });

            var dataService = serviceProvider.GetRequiredService<IDataService>();
            dataService.InicializaDBAsync(serviceProvider).Wait();
        }
    }
}
