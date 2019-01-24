using Core.Domain.Repositories;
using Employees.Domain.ReadModels;

namespace Employees.Domain.Repositories
{
    public interface IProjectRepository : IAggregateRepository<Project, ProjectReadModel>
    {
    }
}
