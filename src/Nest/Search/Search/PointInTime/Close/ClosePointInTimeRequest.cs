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
	[MapsApi("close_point_in_time.json")]
	public partial interface IClosePointInTimeRequest
	{
		/// <summary>
		/// The ID of the point in time to close.
		/// </summary>
		[DataMember(Name = "id")]
		string Id { get; set; }
	}

	/// <inheritdoc cref="ClosePointInTimeRequest" />
	public partial class ClosePointInTimeRequest
	{
		/// <inheritdoc />
		public string Id { get; set; }
	}

	public partial class ClosePointInTimeDescriptor
	{
		string IClosePointInTimeRequest.Id { get; set; }

		/// <inheritdoc cref="IClosePointInTimeRequest.Id" />
		public ClosePointInTimeDescriptor Id(string id) => Assign(id, (a, v) => a.Id = v);
	}
}
