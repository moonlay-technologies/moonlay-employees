using Core.Domain;
using System;

namespace Employees.Domain.Entities
{
    public class Timesheet : EntityBase<Timesheet>
    {
        public Timesheet(Guid identity, Guid teamId) : base(identity)
        {
            TeamId = teamId;
        }

        public Guid TeamId { get; }

        protected override Timesheet GetEntity()
        {
            return this;
        }
    }
}
