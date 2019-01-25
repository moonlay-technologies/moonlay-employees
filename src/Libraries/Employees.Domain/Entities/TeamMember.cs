using Core.Domain;
using Core.Domain.Events;
using System;

namespace Employees.Domain.Entities
{
    public class TeamMember : Entity
    {
        public Guid EmployeeId { get; set; }
        public Guid TeamId { get; set; }
        public Guid PositionId { get; set; }

        public TeamMember(Guid id, Guid employeeId, Guid teamId, Guid positionId)
        {
            Identity = id;
            EmployeeId = employeeId;
            TeamId = teamId;
            PositionId = positionId;

            this.AddDomainEvent(new OnEntityCreated<TeamMember>(this));
        }
    }
}
