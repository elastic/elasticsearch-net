using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The response from deleting a calendar job.
	/// </summary>
	public partial interface IDeleteCalendarJobResponse : IResponse
	{
		[JsonProperty("calendar_id")]
		string CalendarId { get; }

		[JsonProperty("job_ids")]
		IReadOnlyCollection<Id> JobIds { get; }

		[JsonProperty("description")]
		string Description { get; }
	}

	/// <inheritdoc cref="IDeleteCalendarJobResponse" />
	public class DeleteCalendarJobResponse : ResponseBase, IDeleteCalendarJobResponse
	{
		public string CalendarId { get; internal set; }

		public string Description { get; internal set; }

		public IReadOnlyCollection<Id> JobIds { get; internal set; } = EmptyReadOnly<Id>.Collection;
	}
}
