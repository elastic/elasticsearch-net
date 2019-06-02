using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Creates a machine learning calendar.
	/// </summary>
	[MapsApi("ml.put_calendar")]
	public partial interface IPutCalendarRequest
	{
		/// <summary>
		/// A description of the calendar.
		/// </summary>
		[DataMember(Name = "description")]
		string Description { get; set; }
	}

	/// <inheritdoc cref="PutCalendarRequest" />
	public partial class PutCalendarRequest
	{
		/// <inheritdoc />
		public string Description { get; set; }
	}

	public partial class PutCalendarDescriptor
	{
		string IPutCalendarRequest.Description { get; set; }

		/// <inheritdoc cref="IPutCalendarRequest.Description" />
		public PutCalendarDescriptor Description(string description) => Assign(description, (a, v) => a.Description = v);
	}
}
