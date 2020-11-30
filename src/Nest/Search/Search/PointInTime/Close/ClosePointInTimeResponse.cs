// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class ClosePointInTimeResponse : ResponseBase
	{
		/// <summary>
		/// If true, all search contexts associated with the point-in-time id are successfully closed.
		/// </summary>
		[DataMember(Name = "succeeded")]
		public bool Succeeded { get; internal set; }

		/// <summary>
		/// The number of search contexts have been successfully closed.
		/// </summary>
		[DataMember(Name = "num_freed")]
		public int NumberFreed { get; internal set; }
	}
}
