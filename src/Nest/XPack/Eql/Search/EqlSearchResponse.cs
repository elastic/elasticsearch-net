// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.XPack.Eql.Events;

namespace Nest
{
	///// <summary>
	///// A response to an EQL search request.
	///// </summary>
	///// <typeparam name="TDocument">The event type.</typeparam>
	//public interface IEqlSearchResponse<out TDocument> : IResponse where TDocument : class
	//{
	//	/// <summary>
	//	/// Gets the collection of events that matched the search.
	//	/// </summary>
	//	/// <value>
	//	/// The hits.
	//	/// </value>
	//	IReadOnlyCollection<IEvent<TDocument>> Events { get; }

	//	/// <summary>
	//	/// Gets the meta data about the event hits that matched the search query criteria.
	//	/// </summary>
	//	IEventHitsMetadata<TDocument> EventHitsMetadata { get; }

	//	/// <summary>
	//	/// Identifier for the search.
	//	/// </summary>
	//	Id Id { get; }

	//	/// <summary>
	//	/// If true, the response does not contain complete search results.
	//	/// </summary>
	//	bool? IsPartial { get; }

	//	/// <summary>
	//	/// If true, the search request is still executing.
	//	/// </summary>
	//	bool? IsRunning { get; }

	//	/// <summary>
	//	/// Milliseconds it took Elasticsearch to execute the request.
	//	/// </summary>
	//	int Took { get; }

	//	/// <summary>
	//	/// If true, the request timed out before completion.
	//	/// </summary>
	//	bool? TimedOut { get; }

	//	/// <summary>
	//	/// Gets the total number of events matching the search.
	//	/// </summary>
	//	long Total { get; }
	//}

	public class EqlSearchResponse<TDocument> : ResponseBase where TDocument : class
	{
		private IReadOnlyCollection<IEvent<TDocument>> _events;

		/// <inheritdoc />
		[IgnoreDataMember]
		public IReadOnlyCollection<IEvent<TDocument>> Events =>
			_events ??= EventHitsMetadata?.Events ?? EmptyReadOnly<IEvent<TDocument>>.Collection;

		/// <inheritdoc />
		[DataMember(Name = "hits")]
		public IEventHitsMetadata<TDocument> EventHitsMetadata { get; internal set; }

		/// <inheritdoc />
		[DataMember(Name = "id")]
		public Id Id { get; internal set; }

		/// <inheritdoc />
		[DataMember(Name = "is_partial")]
		public bool? IsPartial { get; internal set; }

		/// <inheritdoc />
		[DataMember(Name = "is_running")]
		public bool? IsRunning { get; internal set; }

		/// <inheritdoc />
		[DataMember(Name = "took")]
		public int Took { get; internal set; }

		/// <inheritdoc />
		[DataMember(Name = "timed_out")]
		public bool? TimedOut { get; internal set; }

		/// <inheritdoc />
		[IgnoreDataMember]
		public long Total => EventHitsMetadata?.Total.Value ?? -1;
	}
}
