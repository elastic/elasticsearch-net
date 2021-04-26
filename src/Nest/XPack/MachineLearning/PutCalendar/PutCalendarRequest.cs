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
