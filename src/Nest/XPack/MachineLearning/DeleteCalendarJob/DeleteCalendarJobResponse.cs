using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The response from deleting a calendar job.
	/// </summary>
	public partial interface IDeleteCalendarJobResponse : IResponse
	{
		[DataMember(Name = "calendar_id")]
		string CalendarId { get; }

		[DataMember(Name = "job_ids")]
		IReadOnlyCollection<Id> JobIds { get; }

		[DataMember(Name = "description")]
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
