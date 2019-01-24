using Moonlay.Domain;
using Moonlay.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonlay.Employees.Domain.ReadModels
{
    public class PositionReadModel : ReadModel
    {
        public PositionReadModel(Guid identity)
        {
            Identity = identity;
            TeamMembers = new List<TeamMemberReadModel>();
        }
        public string Name { get; set; }
        public bool Status { get; set; }

        public ICollection<TeamMemberReadModel> TeamMembers { get; set; }
        //public PositionReadModel(Position position) : this(position.Identity)
        //{
        //    this.Name = position.Name;
        //    this.Status = position.Status;
        //}
    }
}
