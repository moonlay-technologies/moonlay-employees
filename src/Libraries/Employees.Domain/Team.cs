using Core.Domain;
using Employees.Domain.ReadModels;
using System;

namespace Employees.Domain
{
    public class Team : AggregateRoot<Team, TeamReadModel>
    {
        public Team(Guid identity) : base(identity)
        {
        }

        public Team(TeamReadModel readModel) : base(readModel)
        {
        }

        protected override Team GetEntity()
        {
            return this;
        }
    }
}
