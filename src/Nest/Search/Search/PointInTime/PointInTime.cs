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
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(PointInTime))]
	public interface IPointInTime
	{
		/// <summary>
		/// The ID of the point in time.
		/// </summary>
		[DataMember(Name = "id")]
		string Id { get; set; }

		/// <summary>
		/// How long to extend the TTL of the point in time.
		/// </summary>
		[DataMember(Name = "keep_alive")]
		Time KeepAlive { get; set; }
	}

	public class PointInTime : IPointInTime
	{
		/// <param name="id">The ID of the point in time.</param>
		public PointInTime(string id) => Id = id;

		/// <param name="id">The ID of the point in time.</param>
		/// <param name="keepAlive">How long to extend the TTL of the point in time.</param>
		public PointInTime(string id, Time keepAlive)
		{
			Id = id;
			KeepAlive = keepAlive;
		}

		/// <inheritdoc />
		public string Id { get; set; }

		/// <inheritdoc />
		public Time KeepAlive { get; set; }
	}

	public class PointInTimeDescriptor : DescriptorBase<PointInTimeDescriptor, IPointInTime>, IPointInTime
	{
		public PointInTimeDescriptor(string id) => Self.Id = id;
		
		string IPointInTime.Id { get; set; }
		Time IPointInTime.KeepAlive { get; set; }
		
		/// <inheritdoc cref="IPointInTime.KeepAlive" />
		public PointInTimeDescriptor KeepAlive(Time id) => Assign(id, (a, v) => a.KeepAlive = v);
	}
}
