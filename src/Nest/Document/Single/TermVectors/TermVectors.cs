using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ITermVectors
	{
		string Index { get; }
		string Type { get; }
		string Id { get; }
		long Version { get; }
		bool Found { get; }
		long Took { get; }
		IReadOnlyDictionary<Field, TermVector> TermVectors { get; }
	}

	public class TermVectorsResult : ITermVectors
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

		[JsonProperty("took")]
		public long Took { get; internal set; }

		[JsonProperty("term_vectors")]
		[JsonConverter(typeof(ResolvableDictionaryJsonConverter<Field, TermVector>))]
		public IReadOnlyDictionary<Field, TermVector> TermVectors { get; internal set; } = EmptyReadOnly<Field, TermVector>.Dictionary;
	}
}
