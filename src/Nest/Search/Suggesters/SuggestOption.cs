using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class SuggestOption<TDocument> where TDocument : class
	{
		/// <summary>
		/// Phrase suggestions only, true if matching documents for the collate query were found,
		/// </summary>
		[JsonProperty("collate_match")]
		public bool CollateMatch { get; internal set; }

		/// <summary>
		/// Completion suggester only, the contexts associated with the completed document
		/// </summary>
		[JsonProperty("contexts")]
		public IDictionary<string, IEnumerable<Context>> Contexts { get; internal set; }

		/// <summary>
		/// Term suggester only
		/// </summary>
		[JsonProperty("freq")]
		public long Frequency { get; set; }

		/// <summary>
		/// Phrase suggester only, higlighted version of text
		/// </summary>
		[JsonProperty("highlighted")]
		public string Highlighted { get; internal set; }

		/// <summary>
		/// Completion suggester only, the id of the completed document
		/// </summary>
		[JsonProperty("_id")]
		public Id Id { get; internal set; }

		/// <summary>
		/// Completion suggester only, the index of the completed document
		/// </summary>
		[JsonProperty("_index")]
		public IndexName Index { get; internal set; }

		[JsonIgnore]
		public double Score => DocumentScore ?? SuggestScore ?? 0;

		/// <summary>
		/// Completion suggester only, the source of the completed document
		/// </summary>
		[JsonProperty("_source")]
		[JsonConverter(typeof(SourceConverter))]
		public TDocument Source { get; internal set; }

		[JsonProperty("text")]
		public string Text { get; internal set; }

		[JsonProperty("_score")]
		internal double? DocumentScore { get; set; }

		[JsonProperty("score")]
		internal double? SuggestScore { get; set; }
	}
}
