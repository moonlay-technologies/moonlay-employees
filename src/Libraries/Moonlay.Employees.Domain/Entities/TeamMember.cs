using Moonlay.Domain;
using Moonlay.Employees.Domain.Events;
using Moonlay.Employees.Domain.ReadModels;
using System;

namespace Moonlay.Employees.Domain.Entities
{
    public class TeamMember : Entity
    {
        public Guid EmployeeId { get; set; }
        public Guid TeamId { get; set; }
        public Guid PositionId { get; set; }
        public TeamMemberReadModel ReadModel {get;set;}

        public TeamMember(Guid id, Guid employeeId, Guid teamId, Guid positionId)
        {
            Identity = id;
            EmployeeId = employeeId;
            TeamId = teamId;
            PositionId = positionId;

            this.AddDomainEvent(new TeamMemberCreated(this));
        }

        public TeamMember(TeamMemberReadModel readModel)
        {
            this.ReadModel = readModel;

            EmployeeId = readModel.EmployeeId;
            TeamId = readModel.TeamId;
            PositionId = readModel.PositionId;
        }
    }
}