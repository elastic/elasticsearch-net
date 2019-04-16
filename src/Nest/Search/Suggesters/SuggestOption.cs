using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class SuggestOption<TDocument> where TDocument : class
	{
		/// <summary>
		/// Phrase suggestions only, true if matching documents for the collate query were found,
		/// </summary>
		[DataMember(Name ="collate_match")]
		public bool CollateMatch { get; internal set; }

		/// <summary>
		/// Completion suggester only, the contexts associated with the completed document
		/// </summary>
		[DataMember(Name ="contexts")]
		public IDictionary<string, IEnumerable<Context>> Contexts { get; internal set; }

		/// <summary>
		/// Term suggester only
		/// </summary>
		[DataMember(Name ="freq")]
		public long Frequency { get; set; }

		/// <summary>
		/// Phrase suggester only, highlighted version of text
		/// </summary>
		[DataMember(Name ="highlighted")]
		public string Highlighted { get; internal set; }

		/// <summary>
		/// Completion suggester only, the id of the completed document
		/// </summary>
		[DataMember(Name ="_id")]
		public Id Id { get; internal set; }

		/// <summary>
		/// Completion suggester only, the index of the completed document
		/// </summary>
		[DataMember(Name ="_index")]
		public IndexName Index { get; internal set; }

		[IgnoreDataMember]
		public double Score => DocumentScore ?? SuggestScore ?? 0;

		/// <summary>
		/// Completion suggester only, the source of the completed document
		/// </summary>
		[DataMember(Name ="_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		public TDocument Source { get; internal set; }

		[DataMember(Name ="text")]
		public string Text { get; internal set; }

		[DataMember(Name = "_score")]
		internal double? DocumentScore { get; set; }

		[DataMember(Name ="score")]
		internal double? SuggestScore { get; set; }
	}
}
