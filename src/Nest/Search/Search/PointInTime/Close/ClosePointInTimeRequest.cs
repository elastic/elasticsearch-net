// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
