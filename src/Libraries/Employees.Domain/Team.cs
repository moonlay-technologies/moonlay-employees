﻿using Core.Domain;
using Employees.Domain.Entities;
using Employees.Domain.ReadModels;
using System;
using System.Collections.Generic;

namespace Employees.Domain
{
    public class Team : AggregateRoot<Team, TeamReadModel>
    {
        public IReadOnlyList<TeamMember> Members { get; private set; }

        public void SetMembers(List<TeamMember> value)
        {
            Members = value;
        }

        public Team(Guid identity) : base(identity)
        {
            Members = new List<TeamMember>();
        }

        public Team(TeamReadModel readModel) : base(readModel)
        {
        }

        protected override Team GetEntity()
        {
            return this;
        }
    }
}
