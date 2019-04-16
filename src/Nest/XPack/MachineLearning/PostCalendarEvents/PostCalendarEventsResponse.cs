using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The response from creating a calendar.
	/// </summary>
	public partial interface IPostCalendarEventsResponse : IResponse
	{
		/// <summary>
		///  A list of one of more scheduled events.
		/// </summary>
		[DataMember(Name = "events")]
		IReadOnlyCollection<ScheduledEvent> Events { get; }
	}

	/// <inheritdoc cref="IPostCalendarEventsResponse" />
	public class PostCalendarEventsResponse : ResponseBase, IPostCalendarEventsResponse
	{
		/// <inheritdoc cref="IPostCalendarEventsResponse.Events"/>
		public IReadOnlyCollection<ScheduledEvent> Events { get; internal set; } = EmptyReadOnly<ScheduledEvent>.Collection;
	}
}
