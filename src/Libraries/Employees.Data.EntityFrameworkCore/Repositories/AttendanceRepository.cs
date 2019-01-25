using Core.Domain.Repositories;
using Employees.Domain.Entities;

namespace Employees.Domain.Repositories
{
    public class AttendanceRepository : EntityRepository<Attendance>, IAttendanceRepository
    {
    }
}
