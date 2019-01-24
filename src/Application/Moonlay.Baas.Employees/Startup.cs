using GraphiQl;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moonlay.Baas.Employees.Models;
using Moonlay.Domain;
using Moonlay.Employees.Application;
using Moonlay.Employees.Domain;
using Moonlay.Employees.EventHandlers;
using Moonlay.Employees.Infrastructure;
using Moonlay.Employees.Repositories;
using System.Reflection;

namespace Moonlay.Baas.Employees
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMediatR(typeof(EmployeeCreatedHandler).GetTypeInfo().Assembly);

            services.AddDbContext<EmployeeDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlopt =>
                {
                    sqlopt.MigrationsAssembly(typeof(Startup).Assembly.FullName);
                });
            });
            services.AddTransient<IEmployeeDbContext>(c => c.GetRequiredService<EmployeeDbContext>());
            services.AddTransient<IUnitOfWork>(c => c.GetRequiredService<EmployeeDbContext>());

            services.AddTransient<INewEmployeeService, NewEmployeeService>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseGraphiQl("/graphql", "/api/graphql");
            app.UseMvc();
        }
    }
}