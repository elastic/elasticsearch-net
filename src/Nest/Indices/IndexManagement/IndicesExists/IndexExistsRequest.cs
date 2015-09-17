using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IIndexExistsRequest : IRequest<IndexExistsRequestParameters> { }

	public partial class IndexExistsRequest : RequestBase<IndexExistsRequestParameters>, IIndexExistsRequest
	{
	}

	[DescriptorFor("IndicesExists")]
	public partial class IndexExistsDescriptor : RequestDescriptorBase<IndexExistsDescriptor, IndexExistsRequestParameters>, IIndexExistsRequest { }
}
