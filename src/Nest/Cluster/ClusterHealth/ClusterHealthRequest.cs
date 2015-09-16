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
        public ClusterHealthRequest()
        { }

        public ClusterHealthRequest(Indices indices)
            : base(p => p.Optional(indices))
        { }
    }

	public partial class ClusterHealthDescriptor : RequestDescriptorBase<ClusterHealthDescriptor, ClusterHealthRequestParameters>, IClusterHealthRequest
    {
        public ClusterHealthDescriptor()
        { }

        public ClusterHealthDescriptor(Indices indices)
            : base(p => p.Optional(indices))
        { }
    }
}
