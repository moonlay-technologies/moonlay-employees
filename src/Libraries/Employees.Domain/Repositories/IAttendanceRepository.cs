using Core.Domain.Repositories;
using Employees.Domain.Entities;

namespace Employees.Domain.Repositories
{
    public interface IAttendanceRepository : IEntityRepository<Attendance>
    {
    }
}
