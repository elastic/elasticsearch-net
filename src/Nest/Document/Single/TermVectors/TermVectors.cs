using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ITermVectors
	{
		bool Found { get; }
		string Id { get; }
		string Index { get; }
		IReadOnlyDictionary<Field, TermVector> TermVectors { get; }
		long Took { get; }
		string Type { get; }
		long Version { get; }
	}

	public class TermVectorsResult : ITermVectors
	{
		[JsonProperty("found")]
		public bool Found { get; internal set; }

		[JsonProperty("_id")]
		public string Id { get; internal set; }

		[JsonProperty("_index")]
		public string Index { get; internal set; }

		[JsonProperty("term_vectors")]
		[JsonConverter(typeof(ResolvableDictionaryJsonConverter<Field, TermVector>))]
		public IReadOnlyDictionary<Field, TermVector> TermVectors { get; internal set; } = EmptyReadOnly<Field, TermVector>.Dictionary;

		[JsonProperty("took")]
		public long Took { get; internal set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }

		[JsonProperty("_version")]
		public long Version { get; internal set; }
	}
}
