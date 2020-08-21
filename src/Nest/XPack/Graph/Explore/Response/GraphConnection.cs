// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class GraphConnection
	{
		[DataMember(Name ="doc_count")]
		public long DocumentCount { get; internal set; }

		[DataMember(Name ="source")]
		public long Source { get; internal set; }

		[DataMember(Name ="target")]
		public long Target { get; internal set; }

		[DataMember(Name ="weight")]
		public double Weight { get; internal set; }
	}
}
