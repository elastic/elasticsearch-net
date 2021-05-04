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
	/// <typeparam name="TDocument">The event type.</typeparam>
	public class EqlSearchResponse<TDocument> : ResponseBase where TDocument : class
	{
		private IReadOnlyCollection<Event<TDocument>> _events;
		private IReadOnlyCollection<Sequence<TDocument>> _sequences;

		/// <summary>
		/// Access the events returned by the search.
		/// </summary>
		[IgnoreDataMember]
		public IReadOnlyCollection<Event<TDocument>> Events =>
			_events ??= EqlHitsMetadata?.Events ?? EmptyReadOnly<Event<TDocument>>.Collection;

		/// <summary>
		/// Access the sequences returned by the search.
		/// </summary>
		[IgnoreDataMember]
		public IReadOnlyCollection<Sequence<TDocument>> Sequences =>
			_sequences ??= EqlHitsMetadata?.Sequences ?? EmptyReadOnly<Sequence<TDocument>>.Collection;

		/// <summary>
		/// Gets the collection of events that matched the search.
		/// </summary>
		/// <value>
		/// The hits.
		/// </value>
		[DataMember(Name = "hits")]
		public EqlHitsMetadata<TDocument> EqlHitsMetadata { get; internal set; }

		/// <summary>
		/// Identifier for the search.
		/// </summary>
		[DataMember(Name = "id")]
		public string Id { get; internal set; }

		/// <summary>
		/// If true, the response does not contain complete search results.
		/// </summary>
		[DataMember(Name = "is_partial")]
		public bool IsPartial { get; internal set; }

		/// <summary>
		/// If true, the search request is still executing.
		/// </summary>
		[DataMember(Name = "is_running")]
		public bool IsRunning { get; internal set; }

		/// <summary>
		/// Milliseconds it took Elasticsearch to execute the request.
		/// </summary>
		[DataMember(Name = "took")]
		public long Took { get; internal set; }

		/// <summary>
		/// If true, the request timed out before completion.
		/// </summary>
		[DataMember(Name = "timed_out")]
		public bool TimedOut { get; internal set; }

		/// <summary>
		/// The total number of hits.
		/// </summary>
		[IgnoreDataMember]
		public long Total => EqlHitsMetadata?.Total.Value ?? -1;
	}
}
