using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The response from creating a calendar.
	/// </summary>
	public partial interface IPutCalendarResponse : IResponse
	{
		[DataMember(Name = "calendar_id")]
		Id CalendarId { get; }

		[DataMember(Name = "description")]
		string Description { get; }

		[DataMember(Name = "job_ids")]
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
