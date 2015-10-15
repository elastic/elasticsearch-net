using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tests.Framework.MockData
{
	//TODO this should be default
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Gender
	{
		Male, Female, NoneOfYourBeeswax
	}
}