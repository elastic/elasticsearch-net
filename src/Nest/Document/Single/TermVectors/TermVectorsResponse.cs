using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ITermVectorsResponse : IResponse
	{
		string Index { get; }
		string Type { get; }
		string Id { get; }
		long Version { get; }
		bool Found { get; }
		long Took { get; }
		IReadOnlyDictionary<string, TermVector> TermVectors { get; }
	}

	[JsonObject]
	public class TermVectorsResponse : ResponseBase, ITermVectorsResponse
	{
		[JsonProperty("_index")]
		public string Index { get; internal set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }

		[JsonProperty("_id")]
		public string Id { get; internal set; }

		[JsonProperty("_version")]
		public long Version { get; internal set; }

		[JsonProperty("found")]
		public bool Found { get; internal set; }

		[JsonProperty(PropertyName = "took")]
		public long Took { get; internal set; }

		[JsonProperty("term_vectors")]
		public IReadOnlyDictionary<string, TermVector> TermVectors { get; internal set; } = EmptyReadOnly<string, TermVector>.Dictionary;
	}
}
