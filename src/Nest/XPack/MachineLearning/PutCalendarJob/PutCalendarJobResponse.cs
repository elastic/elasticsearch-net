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
		Id CalendarId { get; }

		[DataMember(Name = "job_ids")]
		IReadOnlyCollection<Id> JobIds { get; }

		[DataMember(Name = "description")]
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
