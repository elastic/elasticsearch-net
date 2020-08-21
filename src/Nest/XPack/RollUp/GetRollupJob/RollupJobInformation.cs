// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class RollupJobInformation
	{
		[DataMember(Name ="config")]
		public RollupJobConfiguration Config { get; internal set; }

		[DataMember(Name ="stats")]
		public RollupJobStats Stats { get; internal set; }

		[DataMember(Name ="status")]
		public RollupJobStatus Status { get; internal set; }
	}
}
