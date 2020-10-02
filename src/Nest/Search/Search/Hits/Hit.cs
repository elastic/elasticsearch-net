// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Metadata about a hit matching a query
	/// </summary>
	/// <typeparam name="TDocument">The type of the source document</typeparam>
	[InterfaceDataContract]
	public interface IHitMetadata<out TDocument> where TDocument : class
	{
		/// <summary>
		/// The id of the hit
		/// </summary>
		[DataMember(Name = "_id")]
		string Id { get; }

		/// <summary>
		/// The index in which the hit resides
		/// </summary>
		[DataMember(Name = "_index")]
		string Index { get; }

		/// <summary>
		/// The primary term of the hit
		/// </summary>
		[DataMember(Name = "_primary_term")]
		long? PrimaryTerm { get; }

		/// <summary>
		/// The routing value for the hit
		/// </summary>
		[DataMember(Name = "_routing")]
		string Routing { get; }

		/// <summary>
		/// The sequence number for this hit
		/// </summary>
		[DataMember(Name = "_seq_no")]
		long? SequenceNumber { get; }

		/// <summary>
		/// The source document for the hit
		/// </summary>
		[DataMember(Name = "_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		TDocument Source { get; }

		/// <summary>
		/// The version of the hit
		/// </summary>
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
				Index = source.Index,
				Id = source.Id,
				Routing = source.Routing,
				Source = mapper(source.Source)
			};
	}

	/// <summary>
	/// A hit matching a query
	/// </summary>
	/// <typeparam name="TDocument">The type of the source document</typeparam>
	[InterfaceDataContract]
	[ReadAs(typeof(Hit<>))]
	public interface IHit<out TDocument> : IHitMetadata<TDocument>
		where TDocument : class
	{
		/// <summary>
		/// An explanation for why the hit is a match for a query
		/// </summary>
		[DataMember(Name = "_explanation")]
		Explanation Explanation { get; }

		/// <summary>
		/// The individual fields requested for a hit
		/// </summary>
		[DataMember(Name = "fields")]
		FieldValues Fields { get; }

		/// <summary>
		/// The field highlights
		/// </summary>
		[DataMember(Name = "highlight")]
		IReadOnlyDictionary<string, IReadOnlyCollection<string>> Highlight { get; }

		/// <summary>
		/// The inner hits
		/// </summary>
		[DataMember(Name = "inner_hits")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, InnerHitsResult>))]
		IReadOnlyDictionary<string, InnerHitsResult> InnerHits { get; }

		[DataMember(Name = "_nested")]
		NestedIdentity Nested { get; }

		/// <summary>
		/// Which queries the hit is a match for, when a compound query is involved
		/// and named queries used
		/// </summary>
		[DataMember(Name = "matched_queries")]
		IReadOnlyCollection<string> MatchedQueries { get; }

		/// <summary>
		/// The score for the hit in relation to the query
		/// </summary>
		[DataMember(Name = "_score")]
		double? Score { get; }

		/// <summary>
		/// The sort values used in sorting the hit relative to other hits
		/// </summary>
		[DataMember(Name = "sort")]
		IReadOnlyCollection<object> Sorts { get; }
	}

	/// <inheritdoc />
	public class Hit<TDocument> : IHit<TDocument>
		where TDocument : class
	{
		/// <inheritdoc />
		public Explanation Explanation { get; internal set; }
		/// <inheritdoc />
		public FieldValues Fields { get; internal set; }
		/// <inheritdoc />
		public IReadOnlyDictionary<string, IReadOnlyCollection<string>> Highlight { get; internal set; } =
			EmptyReadOnly<string, IReadOnlyCollection<string>>.Dictionary;
		/// <inheritdoc />
		public string Id { get; internal set; }
		/// <inheritdoc />
		public string Index { get; internal set; }
		/// <inheritdoc />
		public IReadOnlyDictionary<string, InnerHitsResult> InnerHits { get; internal set; } =
			EmptyReadOnly<string, InnerHitsResult>.Dictionary;
		/// <inheritdoc />
		public IReadOnlyCollection<string> MatchedQueries { get; internal set; }
			= EmptyReadOnly<string>.Collection;
		/// <inheritdoc />
		public NestedIdentity Nested { get; internal set; }
		/// <inheritdoc />
		public long? PrimaryTerm { get; internal set; }
		/// <inheritdoc />
		public string Routing { get; internal set; }
		/// <inheritdoc />
		public double? Score { get; set; }
		/// <inheritdoc />
		public long? SequenceNumber { get; internal set; }
		/// <inheritdoc />
		public IReadOnlyCollection<object> Sorts { get; internal set; } = EmptyReadOnly<object>.Collection;
		/// <inheritdoc />
		public TDocument Source { get; internal set; }
		/// <inheritdoc />
		public long Version { get; internal set; }
	}
}
