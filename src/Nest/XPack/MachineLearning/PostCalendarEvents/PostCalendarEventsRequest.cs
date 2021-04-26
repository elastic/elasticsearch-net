/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Post scheduled events in a calendar.
	/// </summary>
	[MapsApi("ml.post_calendar_events")]
	public partial interface IPostCalendarEventsRequest
	{
		/// <summary>
		///  A list of one of more scheduled events.
		/// </summary>
		[DataMember(Name = "events")]
		IEnumerable<ScheduledEvent> Events { get; set; }
	}

	public class ScheduledEvent
	{
		/// <summary>
		/// An identifier for the calendar that contains the scheduled event.
		/// </summary>
		[DataMember(Name = "calendar_id")]
		public Id CalendarId { get; set; }

		/// <summary>
		///  A description of the scheduled event.
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; }

		/// <summary>
		/// The timestamp for the beginning of the scheduled event.
		/// </summary>
		[DataMember(Name = "start_time")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset? StartTime { get; set; }

		/// <summary>
		/// The timestamp for the end of the scheduled event.
		/// </summary>
		[DataMember(Name = "end_time")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset? EndTime { get; set; }

		/// <summary>
		/// An automatically-generated identifier for the scheduled event.
		/// </summary>
		[DataMember(Name = "event_id")]
		public Id EventId { get; set; }
	}

	/// <inheritdoc cref="PostCalendarEventsRequest" />
	public partial class PostCalendarEventsRequest
	{
		/// <inheritdoc cref="IPostCalendarEventsRequest.Events"/>
		public IEnumerable<ScheduledEvent> Events { get; set;  }
	}

	public partial class PostCalendarEventsDescriptor
	{
		/// <inheritdoc cref="IPostCalendarEventsRequest.Events"/>
		IEnumerable<ScheduledEvent> IPostCalendarEventsRequest.Events { get; set; }

		/// <inheritdoc cref="IPostCalendarEventsRequest.Events"/>
		public PostCalendarEventsDescriptor Events(IEnumerable<ScheduledEvent> events) => Assign(events, (a, v) => a.Events = v);

		/// <inheritdoc cref="IPostCalendarEventsRequest.Events"/>
		public PostCalendarEventsDescriptor Events(params ScheduledEvent[] events) => Assign(events, (a, v) => a.Events = v);
	}
}
