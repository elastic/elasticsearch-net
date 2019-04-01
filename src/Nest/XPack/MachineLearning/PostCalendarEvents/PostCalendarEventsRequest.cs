using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Post scheduled events in a calendar.
	/// </summary>
	public partial interface IPostCalendarEventsRequest
	{
		/// <summary>
		///  A list of one of more scheduled events.
		/// </summary>
		[JsonProperty("events")]
		IEnumerable<ScheduledEvent> Events { get; set; }
	}

	public class ScheduledEvent
	{
		/// <summary>
		/// An identifier for the calendar that contains the scheduled event.
		/// </summary>
		[JsonProperty("calendar_id")]
		public Id CalendarId { get; set; }

		/// <summary>
		///  A description of the scheduled event.
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		/// The timestamp for the beginning of the scheduled event.
		/// </summary>
		[JsonProperty("start_time")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset? StartTime { get; set; }

		/// <summary>
		/// The timestamp for the end of the scheduled event.
		/// </summary>
		[JsonProperty("end_time")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset? EndTime { get; set; }

		/// <summary>
		/// An automatically-generated identifier for the scheduled event.
		/// </summary>
		[JsonProperty("event_id")]
		public Id EventId { get; set; }
	}

	/// <inheritdoc cref="PostCalendarEventsRequest" />
	public partial class PostCalendarEventsRequest
	{
		/// <inheritdoc cref="IPostCalendarEventsRequest.Events"/>
		[JsonProperty("events")]
		public IEnumerable<ScheduledEvent> Events { get; set;  }
	}

	[DescriptorFor("XpackMlPostCalendarEvents")]
	public partial class PostCalendarEventsDescriptor
	{
		/// <inheritdoc cref="IPostCalendarEventsRequest.Events"/>
		IEnumerable<ScheduledEvent> IPostCalendarEventsRequest.Events { get; set; }

		/// <inheritdoc cref="IPostCalendarEventsRequest.Events"/>
		public PostCalendarEventsDescriptor Events(IEnumerable<ScheduledEvent> events) => Assign(events, (a, v) => a.Events = v);

		/// <inheritdoc cref="IPostCalendarEventsRequest.Events"/>
		public PostCalendarEventsDescriptor Events(params ScheduledEvent[] events) => Assign(events, (a, v) =>
		{
			if (v != null && v.Length == 1 && v[0] is IEnumerable)
				a.Events = ((IEnumerable)v[0]).Cast<ScheduledEvent>();
			else a.Events = v;
		});
	}
}
