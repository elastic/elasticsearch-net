using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IClearCacheRequest : IRequest<ClearCacheRequestParameters> { }
	
	public partial class ClearCacheRequest : RequestBase<ClearCacheRequestParameters>, IClearCacheRequest
	{
	}

	[DescriptorFor("IndicesClearCache")]
	public partial class ClearCacheDescriptor : RequestDescriptorBase<ClearCacheDescriptor, ClearCacheRequestParameters>, IClearCacheRequest
	{
	}
}
