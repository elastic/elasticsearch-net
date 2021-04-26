/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;
using System.Runtime.Serialization;

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
