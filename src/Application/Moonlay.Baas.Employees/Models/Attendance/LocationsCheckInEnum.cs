using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class LocationsCheckInEnum : EnumerationGraphType
    {
        public LocationsCheckInEnum()
        {
            Name = "LocationsCheckIn";
            AddValue("MoonlayHQ", "Moonlay HQ", 2);
            AddValue("Remote", "Remote", 4);
            AddValue("WFH", "Work From Home", 8);
            AddValue("Onsite", "Onsite", 16);
        }
    }
}