using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IHitMetadata<out TDocument> where TDocument : class
	{
		[DataMember(Name = "_id")]
		string Id { get; }

		[DataMember(Name = "_index")]
		string Index { get; }

		[DataMember(Name = "_primary_term")]
		long? PrimaryTerm { get; }

		[DataMember(Name = "_routing")]
		string Routing { get; }

		[DataMember(Name = "_seq_no")]
		long? SequenceNumber { get; }

		[DataMember(Name = "_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		TDocument Source { get; }

		// TODO obsolete or remove type on response
		[DataMember(Name = "_type")]
		string Type { get; }

		[DataMember(Name = "_version")]
		long Version { get; }
	}

	internal static class HitMetadataConversionExtensions
	{
		public static IHitMetadata<TTarget> Copy<TDocument, TTarget>(this IHitMetadata<TDocument> source, Func<TDocument, TTarget> mapper)
			where TDocument : class
			where TTarget : class =>
			new Hit<TTarget>()
			{
				Type = source.Type,
				Index = source.Index,
				Id = source.Id,
				Routing = source.Routing,
				Source = mapper(source.Source)
			};
	}

	[InterfaceDataContract]
	[ReadAs(typeof(Hit<>))]
	public interface IHit<out TDocument> : IHitMetadata<TDocument>
		where TDocument : class
	{
		[DataMember(Name = "_explanation")]
		Explanation Explanation { get; }

		[DataMember(Name = "fields")]
		FieldValues Fields { get; }

		/// <summary> see <see cref="Highlights" /> for easier access into this data structure </summary>
		[DataMember(Name = "highlight")]
		[JsonFormatter(typeof(VerbatimDictionaryKeysBaseFormatter<Dictionary<string, List<string>>, string, List<string>>))]
		Dictionary<string, List<string>> Highlight { get; }

		// TODO investigate this mapping
		/// <summary> This provides easier access into <see cref="Highlight" /> </summary>
		[IgnoreDataMember]
		HighlightFieldDictionary Highlights { get; }

		[DataMember(Name = "inner_hits")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, InnerHitsResult>))]
		IReadOnlyDictionary<string, InnerHitsResult> InnerHits { get; }

		[DataMember(Name = "matched_queries")]
		IReadOnlyCollection<string> MatchedQueries { get; }

		[DataMember(Name = "_nested")]
		NestedIdentity Nested { get; }

		[DataMember(Name = "_score")]
		double? Score { get; }

		[DataMember(Name = "sort")]
		IReadOnlyCollection<object> Sorts { get; }
	}

	public class Hit<TDocument> : IHit<TDocument>
		where TDocument : class
	{
		public Explanation Explanation { get; internal set; }
		public FieldValues Fields { get; internal set; }

		public Dictionary<string, List<string>> Highlight { get; set; }

		public HighlightFieldDictionary Highlights
		{
			get
			{
				if (Highlight == null)
					return new HighlightFieldDictionary();

				var highlights = Highlight.Select(kv => new HighlightHit { DocumentId = Id, Field = kv.Key, Highlights = kv.Value })
					.ToDictionary(k => k.Field, v => v);

				return new HighlightFieldDictionary(highlights);
			}
		}

		public string Id { get; internal set; }
		public string Index { get; internal set; }

		public IReadOnlyDictionary<string, InnerHitsResult> InnerHits { get; internal set; } =
			EmptyReadOnly<string, InnerHitsResult>.Dictionary;

		public IReadOnlyCollection<string> MatchedQueries { get; internal set; }
			= EmptyReadOnly<string>.Collection;

		public NestedIdentity Nested { get; internal set; }
		public long? PrimaryTerm { get; internal set; }
		public string Routing { get; internal set; }
		public double? Score { get; set; }
		public long? SequenceNumber { get; internal set; }

		public IReadOnlyCollection<object> Sorts { get; internal set; }
			= EmptyReadOnly<object>.Collection;

		public TDocument Source { get; internal set; }
		public string Type { get; internal set; }
		public long Version { get; internal set; }
	}
}
