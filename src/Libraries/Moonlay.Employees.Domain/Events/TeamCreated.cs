using MediatR;
using Moonlay.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonlay.Employees.Domain.Events
{
    public class TeamCreated : INotification
    {
        public TeamCreated(Team team)
        {
            Data = team;
        }

        public Team Data { get; set; }
    }
}
