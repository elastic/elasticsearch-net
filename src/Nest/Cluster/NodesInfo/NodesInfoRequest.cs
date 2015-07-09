using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface INodesInfoRequest : INodeIdOptionalPath<NodesInfoRequestParameters>
	{
		IEnumerable<NodesInfoMetric> Metrics { get; set; }
		
	}

	internal static class NodesInfoPathInfo
	{
		public static void Update(ElasticsearchPathInfo<NodesInfoRequestParameters> pathInfo, INodesInfoRequest request)
		{
			if (request.Metrics != null)
				pathInfo.Metric = request.Metrics.Cast<Enum>().GetStringValue();
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
	
	public partial class NodesInfoRequest : NodeIdOptionalPathBase<NodesInfoRequestParameters>, INodesInfoRequest
	{
		public IEnumerable<NodesInfoMetric> Metrics { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<NodesInfoRequestParameters> pathInfo)
		{
			NodesInfoPathInfo.Update(pathInfo, this);
		}

	}

	[DescriptorFor("NodesInfo")]
	public partial class NodesInfoDescriptor : NodeIdOptionalDescriptor<NodesInfoDescriptor, NodesInfoRequestParameters>, INodesInfoRequest
	{
		private INodesInfoRequest Self => this;
		IEnumerable<NodesInfoMetric> INodesInfoRequest.Metrics { get; set; }

		public NodesInfoDescriptor Metrics(params NodesInfoMetric[] metrics)
		{
			Self.Metrics = metrics;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<NodesInfoRequestParameters> pathInfo)
		{
			NodesInfoPathInfo.Update(pathInfo, this);
		}

	}
}
