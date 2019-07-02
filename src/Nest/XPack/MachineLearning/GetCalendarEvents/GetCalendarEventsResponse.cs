using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

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
