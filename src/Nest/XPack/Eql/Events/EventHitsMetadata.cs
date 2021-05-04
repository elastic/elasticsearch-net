// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class EqlHitsMetadata<TEvent> 
		where TEvent : class
	{
		/// <summary>
		/// Contains events matching the query. Each object represents a matching event.
		/// </summary>
		[DataMember(Name = "events")]
		public IReadOnlyCollection<Event<TEvent>> Events { get; internal set; } = EmptyReadOnly<Event<TEvent>>.Collection;

		/// <summary>
		/// Contains event sequences matching the query. Each object represents a matching sequence. This parameter is only returned for EQL queries containing a sequence.
		/// </summary>
		[DataMember(Name = "sequences")]
		public IReadOnlyCollection<Sequence<TEvent>> Sequences { get; internal set; } = EmptyReadOnly<Sequence<TEvent>>.Collection;

		/// <summary>
		/// Metadata about the number of matching events or sequences.
		/// </summary>
		[DataMember(Name = "total")]
		public TotalHits Total { get; internal set; }
	}
}
