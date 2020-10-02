// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Retrieves configuration information for calendars.
	/// </summary>
	public class GetCalendarEventsResponse : ResponseBase
	{
		/// <summary>
		/// Count of scheduled event resources.
		/// </summary>
		[DataMember(Name = "count")]
		public int Count { get; internal set; }

		/// <summary>
		/// 	An array of scheduled event resources.
		/// </summary>
		[DataMember(Name = "events")]
		public IReadOnlyCollection<ScheduledEvent> Events { get; internal set; } = EmptyReadOnly<ScheduledEvent>.Collection;
	}
}
