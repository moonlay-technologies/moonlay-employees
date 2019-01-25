using Core.Data.EntityFrameworkCore;
using Core.Mvc;
using ExtCore.Data.EntityFramework;
using MediatR;
using Microsoft.Extensions.Options;

namespace Moonlay.Baas.Employees.Data
{
    public class EmployeeStorageContext : AppStorageContext
    {
        public EmployeeStorageContext(IOptions<StorageContextOptions> options, IWorkContext workContext, IMediator mediator) : base(options, workContext, mediator)
        {
        }
    }
}
