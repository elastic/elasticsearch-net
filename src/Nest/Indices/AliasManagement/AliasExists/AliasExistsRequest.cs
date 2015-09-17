using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAliasExistsRequest : IRequest<AliasExistsRequestParameters> { }

	public partial class AliasExistsRequest : RequestBase<AliasExistsRequestParameters>, IAliasExistsRequest
	{
	}

	[DescriptorFor("IndicesExistsAlias")]
	public partial class AliasExistsDescriptor : RequestDescriptorBase<AliasExistsDescriptor, AliasExistsRequestParameters>, IAliasExistsRequest
	{
	}
}
