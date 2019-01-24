using Core.Domain.ReadModels;
using System;

namespace Employees.Domain.ReadModels
{
    public class ProjectReadModel : ReadModelBase
    {
        public ProjectReadModel(Guid identity) : base(identity)
        {
        }
    }
}
