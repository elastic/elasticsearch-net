using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRefreshRequest : IRequest<RefreshRequestParameters> { }

	public partial class RefreshRequest : RequestBase<RefreshRequestParameters>, IRefreshRequest
	{
	}

	[DescriptorFor("IndicesRefresh")]
	public partial class RefreshDescriptor : RequestDescriptorBase<RefreshDescriptor, RefreshRequestParameters>, IRefreshRequest
	{
	}
}
