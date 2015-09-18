using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface INodesStatsRequest : IRequest<NodesStatsRequestParameters>
	{
		IEnumerable<NodesStatsMetric> Metrics { get; set; }
		IEnumerable<NodesStatsIndexMetric> IndexMetrics { get; set; }
	}

	internal static class NodesStatsPathInfo
	{
		public static void Update(RouteValues pathInfo, INodesStatsRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.GET;
			if (request.Metrics != null)
				pathInfo.Metric = request.Metrics.Cast<Enum>().GetStringValue();
			if (request.IndexMetrics != null)
				pathInfo.IndexMetric = request.IndexMetrics.Cast<Enum>().GetStringValue();
		}
	}
	
	public partial class NodesStatsRequest : RequestBase<NodesStatsRequestParameters>, INodesStatsRequest
	{
		public IEnumerable<NodesStatsMetric> Metrics { get; set; }
		public IEnumerable<NodesStatsIndexMetric> IndexMetrics { get; set; }

        public NodesStatsRequest() { }

        public NodesStatsRequest(string nodeId)
            : base(p => p.RequiredNodeId(nodeId))
        { }

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RouteValues path)
		{
			NodesStatsPathInfo.Update(path, this);
		}

	}
	[DescriptorFor("NodesStats")]
	public partial class NodesStatsDescriptor : RequestDescriptorBase<NodesStatsDescriptor, NodesStatsRequestParameters>, INodesStatsRequest
	{
		private INodesStatsRequest Self => this;
		IEnumerable<NodesStatsMetric> INodesStatsRequest.Metrics { get; set; }
		IEnumerable<NodesStatsIndexMetric> INodesStatsRequest.IndexMetrics { get; set; }
	
        public NodesStatsDescriptor() { }

        public NodesStatsDescriptor(string nodeId)
            : base(p => p.RequiredNodeId(nodeId))
        { }

	
		public NodesStatsDescriptor Metrics(params NodesStatsMetric[] metrics)
		{
			Self.Metrics = metrics;
			return this;
		}
		public NodesStatsDescriptor IndexMetrics(params NodesStatsIndexMetric[] metrics)
		{
			Self.IndexMetrics = metrics;
			return this;
		}

		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RouteValues path)
		{
			NodesStatsPathInfo.Update(path, this);
		}

	}
}
