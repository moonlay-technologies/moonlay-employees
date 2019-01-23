using Moonlay.Domain;
using Newtonsoft.Json;

namespace Moonlay.Employees.Domain.ValueObjects
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class CompanyId : SingleValueObject<string>
    {
        public CompanyId(string value) : base(value)
        {
        }
    }
}