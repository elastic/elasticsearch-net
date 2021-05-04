// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
