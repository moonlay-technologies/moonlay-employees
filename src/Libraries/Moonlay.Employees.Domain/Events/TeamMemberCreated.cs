using MediatR;
using Moonlay.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonlay.Employees.Domain.Events
{
    public class TeamMemberCreated : INotification
    {
        public TeamMemberCreated(TeamMember teamMember)
        {
            Data = teamMember;
        }

        public TeamMember Data { get; set; }
    }
}
