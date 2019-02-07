using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Retrieves configuration information for calendars.
	/// </summary>
	public interface IGetCalendarsResponse : IResponse
	{
		/// <summary>
		/// The count of calendars.
		/// </summary>
		[JsonProperty("count")]
		long Count { get; }

		/// <summary>
		/// An array of calendar resources.
		/// </summary>
		[JsonProperty("calendars")]
		IReadOnlyCollection<Calendar> Calendars { get; }
	}

	public class Calendar
	{
		[JsonProperty("calendar_id")]
		public Id CalendarId { get; set; }

		[JsonProperty("job_ids")]
		public IReadOnlyCollection<Id> JobIds { get; set; } = EmptyReadOnly<Id>.Collection;

		[JsonProperty("description")]
		public string Description { get; set; }
	}

	public class GetCalendarsResponse : ResponseBase, IGetCalendarsResponse
	{
		/// <inheritdoc cref="IGetCalendarsResponse.Count" />
		public long Count { get; internal set; }

		/// <inheritdoc cref="IGetCalendarsResponse.Calendars" />
		public IReadOnlyCollection<Calendar> Calendars { get; internal set; } = EmptyReadOnly<Calendar>.Collection;
	}
}
