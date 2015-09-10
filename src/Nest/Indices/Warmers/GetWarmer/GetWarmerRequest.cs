using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetWarmerRequest : IIndicesOptionalTypesNamePath<GetWarmerRequestParameters> { }

	public partial class GetWarmerRequest : IndicesOptionalTypesNamePathBase<GetWarmerRequestParameters>, IGetWarmerRequest
	{
	}

	[DescriptorFor("IndicesGetWarmer")]
	public partial class GetWarmerDescriptor : IndicesOptionalTypesNamePathDescriptor<GetWarmerDescriptor, GetWarmerRequestParameters>, IGetWarmerRequest
	{
	}
}
