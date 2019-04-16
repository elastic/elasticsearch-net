using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The response from creating a calendar job.
	/// </summary>
	public partial interface IPutCalendarJobResponse : IResponse
	{
		[DataMember(Name = "calendar_id")]
		string CalendarId { get; }

		[DataMember(Name = "job_ids")]
		IReadOnlyCollection<string> JobIds { get; }

		[DataMember(Name = "description")]
		string Description { get; }
	}

	/// <inheritdoc cref="IPutCalendarJobResponse" />
	public class PutCalendarJobResponse : ResponseBase, IPutCalendarJobResponse
	{
		public string CalendarId { get; internal set; }

		public string Description { get; internal set; }

		public IReadOnlyCollection<string> JobIds { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}
