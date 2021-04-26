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

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Retrieves configuration information for calendars.
	/// </summary>
	[MapsApi("ml.get_calendar_events")]
	public partial interface IGetCalendarEventsRequest
	{
		/// <summary>
		///		Skips a number of events
		/// </summary>
		[DataMember(Name = "from")]
		int? From { get; set; }

		/// <summary>
		///     Specifies a max number of events to get
		/// </summary>
		[DataMember(Name = "size")]
		int? Size { get; set; }
	}

	public partial class GetCalendarEventsRequest
	{
		/// <inheritdoc cref="IGetCalendarEventsRequest.From" />
		public int? From { get; set; }

		/// <inheritdoc cref="IGetCalendarEventsRequest.Size" />
		public int? Size { get; set; }
	}

	public partial class GetCalendarEventsDescriptor
	{
		/// <inheritdoc cref="IGetCalendarEventsRequest.From" />
		int? IGetCalendarEventsRequest.From { get; set; }

		/// <inheritdoc cref="IGetCalendarEventsRequest.Size" />
		int? IGetCalendarEventsRequest.Size { get; set; }

		/// <inheritdoc cref="IGetCalendarEventsRequest.From" />
		public GetCalendarEventsDescriptor From(int? from) => Assign(from, (a, v) => a.From = v);

		/// <inheritdoc cref="IGetCalendarEventsRequest.Size" />
		public GetCalendarEventsDescriptor Size(int? size) => Assign(size, (a, v) => a.Size = v);
	}
}
