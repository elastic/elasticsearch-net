// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// A response to an EQL search request.
	/// </summary>
	/// <typeparam name="TEvent">The event type.</typeparam>
	public interface IEqlSearchResponse<out TEvent> : IResponse where TEvent : class
	{
		/// <summary>
		/// Gets the collection of hits that matched the query.
		/// </summary>
		/// <value>
		/// The hits.
		/// </value>
		IReadOnlyCollection<IHit<TEvent>> Hits { get; }

		/// <summary>
		/// Gets the meta data about the hits that match the search query criteria.
		/// </summary>
		IHitsMetadata<TEvent> HitsMetadata { get; }

		/// <summary>
		/// Identifier for the search.
		/// </summary>
		Id Id { get; }

		/// <summary>
		/// If true, the response does not contain complete search results.
		/// </summary>
		bool? IsPartial { get; }

		/// <summary>
		/// If true, the search request is still executing.
		/// </summary>
		bool? IsRunning { get; }

		/// <summary>
		/// Milliseconds it took Elasticsearch to execute the request.
		/// </summary>
		int Took { get; }

		/// <summary>
		/// If true, the request timed out before completion.
		/// </summary>
		bool? TimedOut { get; }
	}

	public class EqlSearchResponse<TDocument> : ResponseBase, IEqlSearchResponse<TDocument> where TDocument : class
	{
		private IReadOnlyCollection<IHit<TDocument>> _hits;

		/// <inheritdoc />
		[IgnoreDataMember]
		public IReadOnlyCollection<IHit<TDocument>> Hits =>
			_hits ??= HitsMetadata?.Hits ?? EmptyReadOnly<IHit<TDocument>>.Collection;

		/// <inheritdoc />
		[DataMember(Name = "hits")]
		public IHitsMetadata<TDocument> HitsMetadata { get; internal set; }

		/// <inheritdoc />
		[DataMember(Name = "id")]
		public Id Id { get; }

		/// <inheritdoc />
		[DataMember(Name = "is_partial")]
		public bool? IsPartial { get; }

		/// <inheritdoc />
		[DataMember(Name = "is_running")]
		public bool? IsRunning { get; }

		/// <inheritdoc />
		[DataMember(Name = "took")]
		public int Took { get; }

		/// <inheritdoc />
		[DataMember(Name = "timed_out")]
		public bool? TimedOut { get; }
	}
}
