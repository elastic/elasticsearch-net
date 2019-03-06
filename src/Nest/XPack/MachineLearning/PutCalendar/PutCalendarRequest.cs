using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Creates a machine learning calendar.
	/// </summary>
	public partial interface IPutCalendarRequest
	{
		/// <summary>
		/// A description of the calendar.
		/// </summary>
		[JsonProperty("description")]
		string Description { get; set; }
	}

	/// <inheritdoc cref="PutCalendarRequest" />
	public partial class PutCalendarRequest
	{
		/// <inheritdoc />
		public string Description { get; set; }
	}

	[DescriptorFor("XpackMlPutCalendar")]
	public partial class PutCalendarDescriptor
	{
		string IPutCalendarRequest.Description { get; set; }

		/// <inheritdoc cref="IPutCalendarRequest.Description" />
		public PutCalendarDescriptor Description(string description) => Assign(a => a.Description = description);
	}
}
