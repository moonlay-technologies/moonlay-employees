using Core.Domain.Commands;
using System;

namespace Employees.Domain.Commands
{
    public class AddEmployeeCommand : ICommand<Employee>
    {
        public AddEmployeeCommand()
        {

        }

        public AddEmployeeCommand(Guid personId, Guid companyId, DateTimeOffset? regisDate)
        {
            PersonId = personId;
            CompanyId = companyId;
            RegisDate = regisDate;
        }

        public Guid PersonId { get; set; }
        public Guid CompanyId { get; set; }
        public DateTimeOffset? RegisDate { get; set; }
    }
}
