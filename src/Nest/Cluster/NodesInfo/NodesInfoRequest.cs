using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface INodesInfoRequest : IRequest<NodesInfoRequestParameters>
	{
		IEnumerable<NodesInfoMetric> Metrics { get; set; }
	}

	public partial class NodesInfoRequest : RequestBase<NodesInfoRequestParameters>, INodesInfoRequest
	{
		public IEnumerable<NodesInfoMetric> Metrics { get; set; }

        public NodesInfoRequest() { }

        public NodesInfoRequest(string nodeId)
            : base(p => p.RequiredNodeId(nodeId))
        { }
	}

	[DescriptorFor("NodesInfo")]
	public partial class NodesInfoDescriptor : RequestDescriptorBase<NodesInfoDescriptor, NodesInfoRequestParameters>, INodesInfoRequest
	{
		private INodesInfoRequest Self => this;
		IEnumerable<NodesInfoMetric> INodesInfoRequest.Metrics { get; set; }

        public NodesInfoDescriptor() { }

        public NodesInfoDescriptor(string nodeId)
            : base(p => p.RequiredNodeId(nodeId))
        { }
		public NodesInfoDescriptor Metrics(params NodesInfoMetric[] metrics)
		{
			Self.Metrics = metrics;
			return this;
		}
	}
}
