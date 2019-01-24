using Core.Domain.Repositories;
using Employees.Domain.ReadModels;

namespace Employees.Domain.Repositories
{
    public class TeamRepository : AggregateRepostory<Team, TeamReadModel>, ITeamRepository
    {
        protected override Team Map(TeamReadModel readModel)
        {
            return new Team(readModel);
        }
    }
}
