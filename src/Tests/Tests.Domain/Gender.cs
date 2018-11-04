using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tests.Domain
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Gender
	{
		Male,
		Female,
		NoneOfYourBeeswax
	}
}
