using Moonlay.Domain;
using Moonlay.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonlay.Employees.Domain.ReadModels
{
    public class TeamReadModel : ReadModel
    {
        public TeamReadModel(Guid identity)
        {
            this.Identity = identity;
        }
        public TeamReadModel(Team team ) : this(team.Identity)
        {
            this.Name = team.Name;
            this.Status = team.Status;
        }
        public string Name { get; set; }
        public Boolean Status { get; set; }
    }
}
