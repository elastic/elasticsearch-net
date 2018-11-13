using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	public interface IHitMetadata<out TDocument> where TDocument : class
	{
		string Id { get; }
		string Index { get; }

		[Obsolete("No longer returned on indexes created in Elasticsearch 6.x and up, use Routing instead")]
		string Parent { get; }

		string Routing { get; }

		[JsonFormatter(typeof(SourceFormatter<>))]
		TDocument Source { get; }

		string Type { get; }
		long? Version { get; }
	}

	internal static class HitMetadataConversionExtensions
	{
		public static IHitMetadata<TTarget> Copy<TDocument, TTarget>(this IHitMetadata<TDocument> source, Func<TDocument, TTarget> mapper)
			where TDocument : class
			where TTarget : class
		{
			return new Hit<TTarget>()
			{
				Type = source.Type,
				Index = source.Index,
				Id = source.Id,
#pragma warning disable 618
				Routing = source.Routing ?? source.Parent,
				Parent = source.Parent,
#pragma warning restore 618
				Source = mapper(source.Source)
			};
		}
	}

	[ReadAs(typeof(Hit<>))]
	public interface IHit<out TDocument> : IHitMetadata<TDocument> where TDocument : class
	{
		Explanation Explanation { get; }
		FieldValues Fields { get; }
		HighlightFieldDictionary Highlights { get; }
		IReadOnlyDictionary<string, InnerHitsResult> InnerHits { get; }

		IReadOnlyCollection<string> MatchedQueries { get; }

		//search/get related features on hits
		double? Score { get; }
		IReadOnlyCollection<object> Sorts { get; }
	}

	public class Hit<TDocument> : IHit<TDocument> where TDocument : class
	{
		[DataMember(Name ="_explanation")]
		public Explanation Explanation { get; internal set; }

		[DataMember(Name ="fields")]
		public FieldValues Fields { get; internal set; }

		public HighlightFieldDictionary Highlights
		{
			get
			{
				if (_Highlight == null)
					return new HighlightFieldDictionary();

				var highlights = _Highlight.Select(kv => new HighlightHit
					{
						DocumentId = Id,
						Field = kv.Key,
						Highlights = kv.Value
					})
					.ToDictionary(k => k.Field, v => v);

				return new HighlightFieldDictionary(highlights);
			}
		}

		[DataMember(Name ="_id")]
		public string Id { get; internal set; }

		[DataMember(Name ="_index")]
		public string Index { get; internal set; }

		[DataMember(Name ="inner_hits")]
		[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<string, InnerHitsResult>))]
		public IReadOnlyDictionary<string, InnerHitsResult> InnerHits { get; internal set; } =
			EmptyReadOnly<string, InnerHitsResult>.Dictionary;

		[DataMember(Name ="matched_queries")]
		public IReadOnlyCollection<string> MatchedQueries { get; internal set; } = EmptyReadOnly<string>.Collection;

		[DataMember(Name ="_nested")]
		public NestedIdentity Nested { get; internal set; }

		[DataMember(Name ="_parent")]
		[Obsolete("This property is no longer returned on indices created in Elasticsearch 6.0.0 and up, use Routing instead")]
		public string Parent { get; internal set; }

		[DataMember(Name ="_routing")]
		public string Routing { get; internal set; }

		[DataMember(Name ="_score")]
		public double? Score { get; set; }

		[DataMember(Name ="sort")]
		public IReadOnlyCollection<object> Sorts { get; internal set; } = EmptyReadOnly<object>.Collection;

		[DataMember(Name ="_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		public TDocument Source { get; internal set; }

		[DataMember(Name ="_type")]
		public string Type { get; internal set; }

		[DataMember(Name ="_version")]
		public long? Version { get; internal set; }

		[DataMember(Name ="highlight")]
		[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<string, List<string>>))]
		internal Dictionary<string, List<string>> _Highlight { get; set; }
	}
}
