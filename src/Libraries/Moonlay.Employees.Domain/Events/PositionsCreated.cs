using MediatR;
using Moonlay.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonlay.Employees.Domain.Events
{
    public class PositionsCreated : INotification
    {
        public PositionsCreated(Position positions)
        {
            Data = positions;
        }
        public Position Data { get; set; }
    }
}
