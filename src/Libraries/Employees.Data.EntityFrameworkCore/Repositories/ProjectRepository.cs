using Core.Domain.Repositories;
using Employees.Domain.ReadModels;

namespace Employees.Domain.Repositories
{
    public class ProjectRepository : AggregateRepostory<Project, ProjectReadModel>, IProjectRepository
    {
        public ProjectRepository()
        {
        }

        protected override Project Map(ProjectReadModel readModel)
        {
            return new Project(readModel);
        }
    }
}
