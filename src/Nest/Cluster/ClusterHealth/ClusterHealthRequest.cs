using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IClusterHealthRequest : IIndicesOptionalPath<ClusterHealthRequestParameters> { }

	public partial class ClusterHealthRequest : IndicesOptionalPathBase<ClusterHealthRequestParameters>, IClusterHealthRequest { }

	public partial class ClusterHealthDescriptor : IndicesOptionalPathDescriptor<ClusterHealthDescriptor, ClusterHealthRequestParameters>, IClusterHealthRequest { }
}
