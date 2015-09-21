using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface INodesInfoRequest 
	{
		IEnumerable<NodesInfoMetric> Metrics { get; set; }
	}

	public partial class NodesInfoRequest 
	{
		public IEnumerable<NodesInfoMetric> Metrics { get; set; }

		public NodesInfoRequest() { }

		public NodesInfoRequest(string nodeId)
			: base(p => p.RequiredNodeId(nodeId))
		{ }
	}

	[DescriptorFor("NodesInfo")]
	public partial class NodesInfoDescriptor 
	{
		private INodesInfoRequest Self => this;
		IEnumerable<NodesInfoMetric> INodesInfoRequest.Metrics { get; set; }

		public NodesInfoDescriptor Metrics(params NodesInfoMetric[] metrics)
		{
			Self.Metrics = metrics;
			return this;
		}
	}
}
