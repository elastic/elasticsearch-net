using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tests.Domain
{
	[StringEnum, JsonConverter(typeof(StringEnumConverter))]
	public enum Gender
	{
		Male, Female, NoneOfYourBeeswax
	}
}
