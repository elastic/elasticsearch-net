using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The response from creating a calendar job.
	/// </summary>
	public partial interface IPutCalendarJobResponse : IResponse
	{
		[JsonProperty("calendar_id")]
		Id CalendarId { get; }

		[JsonProperty("job_ids")]
		IReadOnlyCollection<Id> JobIds { get; }

		[JsonProperty("description")]
		string Description { get; }
	}

	/// <inheritdoc cref="IPutCalendarJobResponse" />
	public class PutCalendarJobResponse : ResponseBase, IPutCalendarJobResponse
	{
		public Id CalendarId { get; internal set; }

		public string Description { get; internal set; }

		public IReadOnlyCollection<Id> JobIds { get; internal set; } = EmptyReadOnly<Id>.Collection;
	}
}
