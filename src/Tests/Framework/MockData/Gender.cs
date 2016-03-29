using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tests.Framework.MockData
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Gender
	{
		Male, Female, NoneOfYourBeeswax
	}
}