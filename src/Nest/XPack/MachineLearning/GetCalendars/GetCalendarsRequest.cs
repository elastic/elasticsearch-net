// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
