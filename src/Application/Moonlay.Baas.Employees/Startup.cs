using Core.Data.EntityFrameworkCore;
using ExtCore.Data.EntityFramework;
using ExtCore.WebApplication.Extensions;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moonlay.Baas.Employees.Data;
using Moonlay.Baas.Employees.Models;
using System;

namespace Moonlay.Baas.Employees
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        private readonly string _extensionsPath;

        public Startup(IHostingEnvironment hostingEnvironment, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            this.Configuration = configuration;
            this._extensionsPath = hostingEnvironment.ContentRootPath + this.Configuration["Extensions:Path"];
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<StorageContextOptions>(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("Default");

                options.MigrationsAssembly = typeof(Startup).Assembly.FullName;
            }
            );

            services.AddExtCore(_extensionsPath, includingSubpaths: true);

            services.AddMediatR();

            //services.AddKendo();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "MoonlayEmployees API", Version = "v1" });
            });

            //services.AddMediatR(typeof(EmployeeCreatedHandler).GetTypeInfo().Assembly);

            //services.AddDbContext<EmployeeDbContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlopt =>
            //    {
            //        sqlopt.MigrationsAssembly(typeof(Startup).Assembly.FullName);
            //    });
            //});
            //services.AddTransient<IEmployeeDbContext>(c => c.GetRequiredService<EmployeeDbContext>());
            //services.AddTransient<IUnitOfWork>(c => c.GetRequiredService<EmployeeDbContext>());

            //services.AddTransient<INewEmployeeService, NewEmployeeService>();
            //services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            services.AddTransient<IDocumentExecuter, DocumentExecuter>();
            services.AddTransient<EmployeesQuery>();
            services.AddTransient<EmployeesMutation>();

            services.AddTransient<EmployeeType>();
            services.AddTransient<EmployeeInputType>();

            services.AddTransient<AttendanceType>();
            services.AddTransient<CheckInInputType>();
            services.AddTransient<LocationsCheckInEnum>();
            services.AddTransient<CheckOutInputType>();

            services.AddTransient<LeaveType>();
            services.AddTransient<LeaveInputType>();
            services.AddTransient<LeavesTypeEnum>();

            services.AddTransient<TeamType>();
            services.AddTransient<TeamInputType>();

            services.AddTransient<TeamMemberType>();
            services.AddTransient<TeamMemberInputType>();

            services.AddTransient<TimesheetType>();
            services.AddTransient<TimesheetInputType>();

            services.AddTransient<PositionType>();
            services.AddTransient<PositionInputType>();

            services.AddTransient<DepartmentType>();
            services.AddTransient<DepartmentInputType>();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new EmployeesSchema(new FuncDependencyResolver(type => sp.GetService(type))));

            var serviceProvider = services.BuildServiceProvider();

            DesignTimeStorageContextFactory.Initialize(serviceProvider);

            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseExtCore();
            app.UseHttpsRedirection();
            app.UseCookiePolicy();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseGraphiQl("/graphql", "/api/graphql");
        }
    }
}