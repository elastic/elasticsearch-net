using System.Text.Json.Serialization;

namespace Nest
{
	// TODO: Implement properly
	[JsonConverter(typeof(UnionConverter<EpochMillis>))]
	public partial class EpochMillis
	{
		public EpochMillis(string value) { }

		public EpochMillis(int value) { }
	}

	// TODO: Implement properly
	[JsonConverter(typeof(PercentageConverter))]
	public partial class Percentage
	{
		public Percentage(string value) { }

		public Percentage(float value) { }
	}
}
