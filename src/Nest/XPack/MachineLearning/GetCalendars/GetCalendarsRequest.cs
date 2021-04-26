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
