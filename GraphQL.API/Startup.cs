﻿using GraphiQl;
using GraphQL.API.Models;
using GraphQL.API.Types;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHLStats.Api.Helpers;
using NHLStats.Core.Data;
using NHLStats.Data;
using NHLStats.Data.Repositories;

namespace GraphQL.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<NHLStatsContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:NHLStatsDb"]));

            // Contexto
            services.AddHttpContextAccessor();
            services.AddSingleton<ContextServiceLocator>();

            //Repositorios
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<ISkaterStatisticRepository, SkaterStatisticRepository>();

            //Reader
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            //Query
            services.AddSingleton<NHLStatsQuery>();

            //Mutaciones
            //services.AddSingleton<NHLStatsMutation>();

            //Tipo
            services.AddSingleton<PlayerType>();
            //services.AddSingleton<SkaterStatisticType>();
            //services.AddSingleton<PlayerInputType>();

            //Instanciar esquema
            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new NHLStatsSchema(new FuncDependencyResolver(type => sp.GetService(type))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, NHLStatsContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphiQl();
            app.UseMvc();
            db.EnsureSeedData();
        }
    }
}
