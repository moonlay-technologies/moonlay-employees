using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class LeavesTypeEnum : EnumerationGraphType
    {
        public LeavesTypeEnum()
        {
            Name = "LeavesTypeEnum";
            AddValue("PL", "Paid Leave", 2);
            AddValue("CL", "Compliment Leave", 4);
            AddValue("SL", "Sick Leave", 8);
            AddValue("UL", "Unpaid Leave", 16);
        }
    }
}