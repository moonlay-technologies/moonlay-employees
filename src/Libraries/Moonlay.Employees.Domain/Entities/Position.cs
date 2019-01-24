using Moonlay.Domain;
using Moonlay.Employees.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonlay.Employees.Domain.Entities
{
    public class Position : Entity
    {
        public string Name { get; set; }
        public bool Status { get; set; }

        public Position(Guid id, string name, bool status)
        {
            Identity = id;
            Name = name;
            Status = status;

            this.AddDomainEvent(new PositionsCreated(this));
        }
    }
}
