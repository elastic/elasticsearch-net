using System.Text.Json.Serialization;

namespace Nest
{

	[JsonConverter(typeof(UnionConverter<EpochMillis>))]
	public partial class EpochMillis
	{
	}

	[JsonConverter(typeof(PercentageConverter))]
	public partial class Percentage
	{
	}
}
