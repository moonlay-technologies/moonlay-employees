using Core.Domain.ReadModels;
using System;

namespace Employees.Domain.ReadModels
{
    public class TeamReadModel : ReadModelBase
    {
        public TeamReadModel(Guid identity) : base(identity)
        {
        }
    }
}
