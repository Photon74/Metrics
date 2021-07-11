using MediatR;
using MetricsManager.Client;
using MetricsManager.Controllers.Models;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using System;
using MetricsManager.Quartz;
using AutoMapper;
using MetricsManager.Mapper;
using FluentMigrator.Runner;

namespace MetricsManager
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
            services.AddSingleton<AgentsHolder>();
            services.AddMediatR(typeof(Startup));

            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                    .AddTransientHttpErrorPolicy(p => p
                    .WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));

            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IAgentRepository, AgentRepository>();

            var mapper = new MapperConfiguration(mapper => mapper.AddProfile(new MapperProfile())).CreateMapper();
            services.AddSingleton(mapper);

            services.AddFluentMigratorCore()
                    .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(new SQLiteConnectionManager().ConnectionString)
                    .ScanIn(typeof(Startup).Assembly).For.Migrations())
                    .AddLogging(lb => lb
                    .AddFluentMigratorConsole());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            migrationRunner.MigrateUp();
        }
    }
}
