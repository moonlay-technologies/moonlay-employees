using Core.Domain;
using Employees.Domain.ReadModels;
using System;

namespace Employees.Domain
{
    public class Project : AggregateRoot<Project, ProjectReadModel>
    {
        public Project(Guid identity) : base(identity)
        {
        }

        public Project(ProjectReadModel readModel) : base(readModel)
        {
        }

        protected override Project GetEntity()
        {
            return this;
        }
    }
}
