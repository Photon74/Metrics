using AutoMapper;
using FluentMigrator.Runner;
using MediatR;
using MetricsManager.Client;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Repositories;
using MetricsManager.Mapper;
using MetricsManager.Quartz;
using MetricsManager.Quartz.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;

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
            services.AddFluentMigratorCore()
                    .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(new SQLiteConnectionManager().ConnectionString)
                    .ScanIn(typeof(Startup).Assembly).For.Migrations())
                    .AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            services.AddControllers();
            //services.AddSingleton<AgentsHolder>();
            services.AddMediatR(typeof(Startup));

            var mapper = new MapperConfiguration(mapper => mapper.AddProfile(new MapperProfile())).CreateMapper();
            services.AddSingleton(mapper);

            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                    .AddTransientHttpErrorPolicy(p => p
                    .WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));

            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            //services.AddSingleton<CpuMetricsJob>();
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(CpuMetricsJob),
            //    cronExpression: "0/5 * * * * ?"));

            //services.AddSingleton<RamMetricsJob>();
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(RamMetricsJob),
            //    cronExpression: "0/5 * * * * ?"));

            //services.AddSingleton<HddMetricsJob>();
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(HddMetricsJob),
            //    cronExpression: "0/5 * * * * ?"));

            //services.AddSingleton<NetworkMetricsJob>();
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(NetworkMetricsJob),
            //    cronExpression: "0/5 * * * * ?"));

            services.AddSingleton<DotNetMetricsJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(DotNetMetricsJob),
                cronExpression: "0/5 * * * * ?"));

            services.AddSingleton<IDBConnectionManager, SQLiteConnectionManager>();
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IAgentRepository, AgentRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IMigrationRunner migrationRunner)
        {
            migrationRunner.MigrateUp();

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

        }
    }
}
