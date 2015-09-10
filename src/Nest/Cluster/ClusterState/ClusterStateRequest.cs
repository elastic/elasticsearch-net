using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IClusterStateRequest : IIndicesOptionalPath<ClusterStateRequestParameters>
	{
		IEnumerable<ClusterStateMetric> Metrics { get; set; }
	}

	public partial class ClusterStateRequest : IndicesOptionalPathBase<ClusterStateRequestParameters>, IClusterStateRequest
	{
		public IEnumerable<ClusterStateMetric> Metrics { get; set; }
	}


	public partial class ClusterStateDescriptor : IndicesOptionalPathDescriptor<ClusterStateDescriptor, ClusterStateRequestParameters>, IClusterStateRequest
	{
		private IClusterStateRequest Self => this;

		IEnumerable<ClusterStateMetric> IClusterStateRequest.Metrics { get; set; }
		public ClusterStateDescriptor Metrics(params ClusterStateMetric[] metrics)
		{
			Self.Metrics = metrics;
			return this;
		}
	}
}
