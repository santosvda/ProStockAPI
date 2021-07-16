using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ProStock.Repository;
using ProStock.Repository.Interfaces;
using ProStock.Repository.Repositorys;

namespace ProStock.API
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
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            //configuração de conexão com o banco
            services.AddDbContext<ProStockContext>(options =>
            {
                //options.UseLazyLoadingProxies().UseMySql(Configuration.GetConnectionString(Configuration["Database:DefaultConnection"]), x =>
                options.UseMySql(Configuration.GetConnectionString(Configuration["Database:DefaultConnection"]), x =>
                {
                    x.ServerVersion(new Version(5, 7, 21), ServerType.MySql);
                    x.MigrationsHistoryTable("EfMigrations");
                    x.MigrationsAssembly(migrationsAssembly);
                    
                });
            });

            //Configurando JWT (obs: bearer = portador en-pt)
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,//validação do emissor (emissor = api)
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),//chave de criptografia
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Add todas as ocntrollers a necessidade de autenticação
            //cria uma politica para que sempre que uma controller for chamada automaticamente respeitar a configuração criada (politica de autenticação)
            services.AddMvc(options => {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                     .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = 
            Newtonsoft.Json.ReferenceLoopHandling.Ignore); //controla redundancia em relação ao retorno da serialização dos itens
            
            //Informar a aplicação que a mesma trabalha com AutoMapper
            /*
                    *Domain*    *API*  
                Ex: Evento <--> EventoDto
                DTO = Data transfer object
            */
            services.AddAutoMapper();

            //sempre que precisar do IProAgilRepository, impletamenta o ProAgilRepository
            services.AddScoped<IProStockRepository, ProStockRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IEstoqueRepository, EstoqueRepository>();
            services.AddScoped<ILojaRepository, LojaRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IVendaRepository, VendaRepository>();
            //Configuração de permisão - CORS
            services.AddCors();

            //Adicionando swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title = "ProStock", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            InitializeDatabase(app);

            app.UseAuthentication(); //informa que a api precisa ser autenticada

            //app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var audieDbContext = serviceScope.ServiceProvider.GetRequiredService<ProStockContext>();
                audieDbContext.Database.Migrate();
            }
        }
    }
}
