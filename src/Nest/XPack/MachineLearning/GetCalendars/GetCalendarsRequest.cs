using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Retrieves configuration information for calendars.
	/// </summary>
	[MapsApi("ml.get_calendars")]
	public partial interface IGetCalendarsRequest
	{
		/// <summary>
		/// Specifies pagination for the calendars
		/// </summary>
		[DataMember(Name = "page")]
		IPage Page { get; set; }
	}

	public partial class GetCalendarsRequest
	{
		/// <inheritdoc cref="IGetCalendarsRequest.Page" />
		public IPage Page { get; set; }
	}

	public partial class GetCalendarsDescriptor
	{
		/// <inheritdoc cref="IGetCalendarsRequest.Page" />
		IPage IGetCalendarsRequest.Page { get; set; }

		/// <inheritdoc cref="IGetCalendarsRequest.Page" />
		public GetCalendarsDescriptor Page(Func<PageDescriptor, IPage> selector) => Assign(selector, (a, v) => a.Page = v?.Invoke(new PageDescriptor()));
	}
}
