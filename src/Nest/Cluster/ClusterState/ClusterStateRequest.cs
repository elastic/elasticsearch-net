using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IClusterStateRequest : IRequest<ClusterStateRequestParameters>
	{
	}

	public partial class ClusterStateRequest : RequestBase<ClusterStateRequestParameters>, IClusterStateRequest
	{
        public ClusterStateRequest() { }

        public ClusterStateRequest(Indices indices) : base(p => p.Optional(indices)) { }

        public ClusterStateRequest(Indices indices, IEnumerable<ClusterStateMetric> metrics)
			: base(p => p.Optional(indices).Optional(metrics)) { }

        public ClusterStateRequest(Indices indices, params ClusterStateMetric[] metrics)
			: base(p => p.Optional(indices).Optional(metrics)) { }
	}


	public partial class ClusterStateDescriptor 
		: RequestDescriptorBase<ClusterStateDescriptor, ClusterStateRequestParameters, IClusterStateRequest>, IClusterStateRequest
	{
		public ClusterStateDescriptor Metrics(params ClusterStateMetric[] metrics) => Assign(a => a.RouteValues.Required(metrics));
		public ClusterStateDescriptor Metrics(IEnumerable<ClusterStateMetric> metrics) => Assign(a => a.RouteValues.Required(metrics));
	}
}
