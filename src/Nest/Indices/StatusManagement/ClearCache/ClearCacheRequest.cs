using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IClearCacheRequest : IIndicesOptionalPath<ClearCacheRequestParameters> { }
	
	public partial class ClearCacheRequest : IndicesOptionalPathBase<ClearCacheRequestParameters>, IClearCacheRequest
	{
	}

	[DescriptorFor("IndicesClearCache")]
	public partial class ClearCacheDescriptor : IndicesOptionalPathDescriptor<ClearCacheDescriptor, ClearCacheRequestParameters>, IClearCacheRequest
	{
	}
}
