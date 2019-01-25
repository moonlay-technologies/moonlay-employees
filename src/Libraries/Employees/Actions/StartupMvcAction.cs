using ExtCore.Infrastructure.Actions;
using ExtCore.Mvc.Infrastructure.Actions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Actions
{
    public class StartupMvcAction : IConfigureServicesAction, IAddMvcAction, IUseMvcAction
    {
        public int Priority => 1000;



        public void Execute(IServiceCollection services, IServiceProvider sp)
        {

        }

        public void Execute(IMvcBuilder builder, IServiceProvider sp)
        {

        }

        public void Execute(IRouteBuilder route, IServiceProvider sp)
        {
        }
    }
}
