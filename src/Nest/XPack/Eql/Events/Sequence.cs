// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class Sequence<TEvent> where TEvent : class
	{
		/// <summary>
		/// Contains events matching the query. Each object represents a matching event.
		/// </summary>
		[DataMember(Name = "events")]
		public IReadOnlyCollection<Event<TEvent>> Events { get; internal set; } = EmptyReadOnly<Event<TEvent>>.Collection;

		/// <summary>
		/// Shared field values used to constrain matches in the sequence. These are defined using the by keyword in the EQL query syntax.
		/// </summary>
		[DataMember(Name = "join_keys")]
		public IReadOnlyCollection<object> JoinKeys { get; internal set; } = EmptyReadOnly<object>.Collection;
	}
}
