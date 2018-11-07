using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class InstantGet<TDocument> where TDocument : class
	{
		[JsonProperty("fields")]
		public FieldValues Fields { get; internal set; }

		[JsonProperty("found")]
		public bool Found { get; internal set; }

		[JsonProperty("_source")]
		[JsonConverter(typeof(SourceConverter))]
		public TDocument Source { get; internal set; }
	}
}
