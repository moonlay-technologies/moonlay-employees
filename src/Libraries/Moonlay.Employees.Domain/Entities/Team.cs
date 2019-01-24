using Moonlay.Domain;
using Moonlay.Employees.Domain.Events;
using Moonlay.Employees.Domain.ReadModels;
using System;

namespace Moonlay.Employees.Domain.Entities
{
    public sealed class Team : Entity
    {
        public string Name { get; set; }
        public Boolean Status { get; set; }
        public TeamReadModel ReadModel { get; }

        public Team(Guid id, string name, Boolean status)
        {
            Identity = id;
            Name = name;
            Status = status;

            this.AddDomainEvent(new TeamCreated(this));
        }
        public Team(TeamReadModel readModel)
        {
            this.ReadModel = readModel;

            Name = readModel.Name;
            Status = readModel.Status;
        }
    }
}