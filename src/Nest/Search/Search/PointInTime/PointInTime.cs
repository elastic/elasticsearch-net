// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
