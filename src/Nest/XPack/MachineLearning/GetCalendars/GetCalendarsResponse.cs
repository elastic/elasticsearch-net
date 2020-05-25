// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Retrieves configuration information for calendars.
	/// </summary>
	public class GetCalendarsResponse : ResponseBase
	{
		/// <summary>
		/// The count of calendars.
		/// </summary>
		[DataMember(Name = "count")]
		public long Count { get; internal set; }

		/// <summary>
		/// An array of calendar resources.
		/// </summary>
		[DataMember(Name = "calendars")]
		public IReadOnlyCollection<Calendar> Calendars { get; internal set; } = EmptyReadOnly<Calendar>.Collection;
	}

	public class Calendar
	{
		[DataMember(Name = "calendar_id")]
		public string CalendarId { get; set; }

		[DataMember(Name = "job_ids")]
		public IReadOnlyCollection<string> JobIds { get; set; } = EmptyReadOnly<string>.Collection;

		[DataMember(Name = "description")]
		public string Description { get; set; }
	}

}
