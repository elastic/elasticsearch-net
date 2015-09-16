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
		IEnumerable<ClusterStateMetric> Metrics { get; set; }
	}

	public partial class ClusterStateRequest : RequestBase<ClusterStateRequestParameters>, IClusterStateRequest
	{
        public ClusterStateRequest() { }

        public ClusterStateRequest(Indices indices)
            : base(p => p.Optional(indices))
        { }
		
        public IEnumerable<ClusterStateMetric> Metrics { get; set; }
	}


	public partial class ClusterStateDescriptor : RequestDescriptorBase<ClusterStateDescriptor, ClusterStateRequestParameters>, IClusterStateRequest
	{
		private IClusterStateRequest Self => this;

        public ClusterStateDescriptor() { }

        public ClusterStateDescriptor(Indices indices)
            : base(p => p.Optional(indices))
        { }
        
        IEnumerable<ClusterStateMetric> IClusterStateRequest.Metrics { get; set; }
		public ClusterStateDescriptor Metrics(params ClusterStateMetric[] metrics)
		{
			Self.Metrics = metrics;
			return this;
		}
	}
}
