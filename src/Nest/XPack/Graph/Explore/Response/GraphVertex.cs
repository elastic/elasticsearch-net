// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class GraphVertex
	{
		[DataMember(Name ="depth")]
		public long Depth { get; internal set; }

		[DataMember(Name ="field")]
		public string Field { get; internal set; }

		[DataMember(Name ="term")]
		public string Term { get; internal set; }

		[DataMember(Name ="weight")]
		public double Weight { get; internal set; }
	}
}
