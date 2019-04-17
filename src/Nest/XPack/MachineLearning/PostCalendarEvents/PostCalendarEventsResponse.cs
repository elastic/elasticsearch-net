using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The response from creating a calendar.
	/// </summary>
	public class PostCalendarEventsResponse : ResponseBase
	{
		/// <summary>
		///  A list of one of more scheduled events.
		/// </summary>
		[DataMember(Name = "events")]
		public IReadOnlyCollection<ScheduledEvent> Events { get; internal set; } = EmptyReadOnly<ScheduledEvent>.Collection;
	}
}
