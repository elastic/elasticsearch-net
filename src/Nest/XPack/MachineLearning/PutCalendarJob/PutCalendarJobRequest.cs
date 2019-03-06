using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Adds a job to a calendar.
	/// </summary>
	public partial interface IPutCalendarJobRequest { }

	/// <inheritdoc cref="PutCalendarJobRequest" />
	public partial class PutCalendarJobRequest { }

	[DescriptorFor("XpackMlPutCalendarJob")]
	public partial class PutCalendarJobDescriptor { }
}
