using Moonlay.Domain;
using Moonlay.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonlay.Employees.Domain.ReadModels
{
    public class TeamMemberReadModel : ReadModel
    {

        public TeamMemberReadModel(Guid identity)
        {
            Identity = identity;
        }

        public Guid EmployeeId { get; set; }
        public Guid TeamId { get; set; }

        public Guid PositionId { get; set; }
        public virtual PositionReadModel Position {get;set;}

        public TeamMemberReadModel(TeamMember teamMember) : this(teamMember.Identity)
        {
            this.EmployeeId = Guid.Parse(teamMember.EmployeeId.ToString());
            this.TeamId = Guid.Parse(teamMember.TeamId.ToString());
            this.PositionId = Guid.Parse(teamMember.PositionId.ToString());
        }
    }
}
