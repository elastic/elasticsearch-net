using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Retrieves configuration information for calendars.
	/// </summary>
	public partial interface IGetCalendarEventsRequest
	{
		/// <summary>
		///		Skips a number of events
		/// </summary>
		[JsonProperty("from")]
		int? From { get; set; }

		/// <summary>
		///     Specifies a max number of events to get
		/// </summary>
		[JsonProperty("size")]
		int? Size { get; set; }
	}

	public partial class GetCalendarEventsRequest
	{
		/// <inheritdoc cref="IGetCalendarEventsRequest.From" />
		public int? From { get; set; }

		/// <inheritdoc cref="IGetCalendarEventsRequest.Size" />
		public int? Size { get; set; }
	}

	[DescriptorFor("XpackMlGetCalendarEvents")]
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
