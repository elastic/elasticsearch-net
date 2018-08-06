using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tests.Framework.MockData
{
	[StringEnum, JsonConverter(typeof(StringEnumConverter))]
	public enum Gender
	{
		Male, Female, NoneOfYourBeeswax
	}
}
