using System.Collections.Generic;
using Newtonsoft.Json;

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
		[JsonProperty("count")]
		int Count { get; }

		/// <summary>
		/// 	An array of scheduled event resources.
		/// </summary>
		[JsonProperty("events")]
		IEnumerable<ScheduledEvent> Events { get; }
	}

	public class GetCalendarEventsResponse : ResponseBase, IGetCalendarEventsResponse
	{
		/// <inheritdoc cref="IGetCalendarEventsResponse.Count"/>
		[JsonProperty("count")]
		public int Count { get; internal set; }

		/// <inheritdoc cref="IGetCalendarEventsResponse.Events"/>
		[JsonProperty("events")]
		public IEnumerable<ScheduledEvent> Events { get; internal set; }
	}
}
