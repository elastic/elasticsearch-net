using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SuggestOption<>))]
	public interface ISuggestOption<out TDocument> where TDocument : class
	{
		/// <summary>
		/// Phrase suggestions only, true if matching documents for the collate query were found,
		/// </summary>
		[DataMember(Name ="collate_match")]
		bool CollateMatch { get; }

		/// <summary>
		/// Completion suggester only, the contexts associated with the completed document
		/// </summary>
		[DataMember(Name ="contexts")]
		IDictionary<string, IEnumerable<Context>> Contexts { get; }

		/// <summary>
		/// Term suggester only
		/// </summary>
		[DataMember(Name ="freq")]
		long Frequency { get; set; }

		/// <summary>
		/// Phrase suggester only, highlighted version of text
		/// </summary>
		[DataMember(Name ="highlighted")]
		string Highlighted { get; }

		/// <summary>
		/// Completion suggester only, the id of the completed document
		/// </summary>
		[DataMember(Name ="_id")]
		Id Id { get; }

		/// <summary>
		/// Completion suggester only, the index of the completed document
		/// </summary>
		[DataMember(Name ="_index")]
		IndexName Index { get; }

		/// <summary> Either the <see cref="DocumentScore"/> or the <see cref="SuggestScore"/></summary>
		[IgnoreDataMember]
		double Score { get; }

		/// <summary>
		/// Completion suggester only, the source of the completed document
		/// </summary>
		[DataMember(Name ="_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		TDocument Source { get; }

		[DataMember(Name ="text")]
		string Text { get; }

		// TODO this should be reported to elastic/elasticsearch
		[DataMember(Name = "_score")]
		double? DocumentScore { get; set; }

		[DataMember(Name ="score")]
		double? SuggestScore { get; set; }
	}

	/// <inheritdoc cref="ISuggestOption{TDocument}"/>
	public class SuggestOption<TDocument> where TDocument : class
	{
		/// <inheritdoc cref="ISuggestOption{TDocument}.CollateMatch"/>
		[DataMember(Name ="collate_match")]
		public bool CollateMatch { get; internal set; }

		/// <inheritdoc cref="ISuggestOption{TDocument}.Contexts"/>
		[DataMember(Name ="contexts")]
		public IDictionary<string, IEnumerable<Context>> Contexts { get; internal set; }

		/// <inheritdoc cref="ISuggestOption{TDocument}.Frequency"/>
		[DataMember(Name ="freq")]
		public long Frequency { get; set; }

		/// <inheritdoc cref="ISuggestOption{TDocument}.Highlighted"/>
		[DataMember(Name ="highlighted")]
		public string Highlighted { get; internal set; }

		/// <inheritdoc cref="ISuggestOption{TDocument}.Id"/>
		[DataMember(Name ="_id")]
		public Id Id { get; internal set; }

		/// <inheritdoc cref="ISuggestOption{TDocument}.Index"/>
		[DataMember(Name ="_index")]
		public IndexName Index { get; internal set; }

		/// <inheritdoc cref="ISuggestOption{TDocument}.Score"/>
		[IgnoreDataMember]
		public double Score => DocumentScore ?? SuggestScore ?? 0;

		/// <inheritdoc cref="ISuggestOption{TDocument}.Source"/>
		[DataMember(Name ="_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		public TDocument Source { get; internal set; }

		/// <inheritdoc cref="ISuggestOption{TDocument}.Text"/>
		[DataMember(Name ="text")]
		public string Text { get; internal set; }

		/// <inheritdoc cref="ISuggestOption{TDocument}.DocumentScore"/>
		[DataMember(Name = "_score")]
		internal double? DocumentScore { get; set; }

		/// <inheritdoc cref="ISuggestOption{TDocument}.SuggestScore"/>
		[DataMember(Name ="score")]
		internal double? SuggestScore { get; set; }
	}
}
