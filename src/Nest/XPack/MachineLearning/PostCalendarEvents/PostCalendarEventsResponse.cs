using System.Collections.Generic;
using Newtonsoft.Json;

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
		[JsonProperty("events")]
		IEnumerable<ScheduledEvent> Events { get; }
	}

	/// <inheritdoc cref="IPostCalendarEventsResponse" />
	public class PostCalendarEventsResponse : ResponseBase, IPostCalendarEventsResponse
	{
		/// <inheritdoc cref="IPostCalendarEventsResponse.Events"/>
		[JsonProperty("events")]
		public IEnumerable<ScheduledEvent> Events { get; internal set;  }
	}
}
