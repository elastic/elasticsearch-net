using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRefreshRequest : IIndicesOptionalPath<RefreshRequestParameters> { }

	public partial class RefreshRequest : IndicesOptionalPathBase<RefreshRequestParameters>, IRefreshRequest
	{
	}

	[DescriptorFor("IndicesRefresh")]
	public partial class RefreshDescriptor : IndicesOptionalPathDescriptor<RefreshDescriptor, RefreshRequestParameters>, IRefreshRequest
	{
	}
}
