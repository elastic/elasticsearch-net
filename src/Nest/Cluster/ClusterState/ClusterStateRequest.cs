using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IClusterStateRequest { }

	public partial class ClusterStateRequest 
	{
		public ClusterStateRequest() { }

		public ClusterStateRequest(Indices indices) : base(p => p.Optional(indices)) { }

		public ClusterStateRequest(Indices indices, IEnumerable<ClusterStateMetric> metrics)
			: base(p => p.Optional(indices).Optional(metrics))
		{ }

		public ClusterStateRequest(Indices indices, params ClusterStateMetric[] metrics)
			: base(p => p.Optional(indices).Optional(metrics))
		{ }
	}


	public partial class ClusterStateDescriptor 
	{
		public ClusterStateDescriptor Metrics(params ClusterStateMetric[] metrics) => Assign(a => a.RouteValues.Required(metrics));
		public ClusterStateDescriptor Metrics(IEnumerable<ClusterStateMetric> metrics) => Assign(a => a.RouteValues.Required(metrics));
	}
}
