using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The response from creating a calendar.
	/// </summary>
	public partial interface IPutCalendarResponse : IResponse
	{
		[JsonProperty("calendar_id")]
		Id CalendarId { get; }

		[JsonProperty("description")]
		string Description { get; }

		[JsonProperty("job_ids")]
		IReadOnlyCollection<Id> JobIds { get; }
	}

	/// <inheritdoc cref="IPutCalendarResponse" />
	public class PutCalendarResponse : ResponseBase, IPutCalendarResponse
	{
		public Id CalendarId { get; internal set; }

		public string Description { get; internal set; }

		public IReadOnlyCollection<Id> JobIds { get; internal set; } = EmptyReadOnly<Id>.Collection;
	}
}
