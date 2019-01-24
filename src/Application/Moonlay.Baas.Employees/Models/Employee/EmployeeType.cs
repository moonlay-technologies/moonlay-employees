using GraphQL.Types;
using Moonlay.Employees.Domain;
using Moonlay.Employees.Domain.Entities;
using System.Linq;

namespace Moonlay.Baas.Employees.Models
{
    public class EmployeeType : ObjectGraphType<Employee>
    {
        public EmployeeType()
        {
            Field<StringGraphType>("identity", resolve: context => context.Source.Identity.ToString());
            Field<StringGraphType>("personId", resolve: context => context.Source.PersonId.Value.ToString());
            Field<StringGraphType>("companyId", resolve: context => context.Source.CompanyId.Value.ToString());
            Field<DateTimeOffsetGraphType>("regisDate", resolve: context => context.Source.RegisDate);
            Field<DateTimeOffsetGraphType>("resignDate", resolve: context => context.Source.ResignDate);
            Field<StringGraphType>("attendances", resolve: context => context.Source.Attendances.Select(o => new Attendance(o.Identity,o.EmployeeId, o.CheckInDate, o.LocationCheckIn, o.CheckOutDate, o.Duration)).ToList());
            Field<StringGraphType>("day", resolve: context => context.Source.Day);
            Field<StringGraphType>("month", resolve: context => context.Source.Month);
            Field<StringGraphType>("year", resolve: context => context.Source.Year);
        }
    }
}