using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Retrieves configuration information for calendars.
	/// </summary>
	public interface IGetCalendarEventsResponse : IResponse
	{
		/// <summary>
		/// Count of scheduled event resources.
		/// </summary>
		[DataMember(Name = "count")]
		int Count { get; }

		/// <summary>
		/// 	An array of scheduled event resources.
		/// </summary>
		[DataMember(Name = "events")]
		IReadOnlyCollection<ScheduledEvent> Events { get; }
	}

	public class GetCalendarEventsResponse : ResponseBase, IGetCalendarEventsResponse
	{
		/// <inheritdoc cref="IGetCalendarEventsResponse.Count"/>
		public int Count { get; internal set; }

		/// <inheritdoc cref="IGetCalendarEventsResponse.Events"/>
		public IReadOnlyCollection<ScheduledEvent> Events { get; internal set; } = EmptyReadOnly<ScheduledEvent>.Collection;
	}
}
