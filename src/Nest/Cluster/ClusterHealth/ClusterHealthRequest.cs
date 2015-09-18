using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IClusterHealthRequest : IRequest<ClusterHealthRequestParameters> { }

	public partial class ClusterHealthRequest : RequestBase<ClusterHealthRequestParameters>, IClusterHealthRequest
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

	public partial class ClusterHealthDescriptor : RequestDescriptorBase<ClusterHealthDescriptor, ClusterHealthRequestParameters>, IClusterHealthRequest
    {

    }
}
