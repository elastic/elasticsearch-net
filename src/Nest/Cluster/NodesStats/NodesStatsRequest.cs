using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface INodesStatsRequest 
	{
		IEnumerable<NodesStatsMetric> Metrics { get; set; }
		IEnumerable<NodesStatsIndexMetric> IndexMetrics { get; set; }
	}

	public partial class NodesStatsRequest 
	{
		public IEnumerable<NodesStatsMetric> Metrics { get; set; }
		public IEnumerable<NodesStatsIndexMetric> IndexMetrics { get; set; }
	}
	[DescriptorFor("NodesStats")]
	public partial class NodesStatsDescriptor 
	{
		private INodesStatsRequest Self => this;
		IEnumerable<NodesStatsMetric> INodesStatsRequest.Metrics { get; set; }
		IEnumerable<NodesStatsIndexMetric> INodesStatsRequest.IndexMetrics { get; set; }

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
	}
}
