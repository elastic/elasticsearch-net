using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IClusterHealthRequest { }

	public partial class ClusterHealthRequest 
	{
		/// <summary>
		/// /_cluster/health
		/// </summary>
		public ClusterHealthRequest() { }

		/// <summary>
		/// /_cluster/health/{index}
		/// </summary>
		public ClusterHealthRequest(Indices indices) : base(p => p.Optional(indices)) { }
	}

	public partial class ClusterHealthDescriptor { 
	}
}
