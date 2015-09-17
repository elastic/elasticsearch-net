using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetWarmerRequest : IRequest<GetWarmerRequestParameters> { }

	public partial class GetWarmerRequest : RequestBase<GetWarmerRequestParameters>, IGetWarmerRequest
	{
	}

	[DescriptorFor("IndicesGetWarmer")]
	public partial class GetWarmerDescriptor : RequestDescriptorBase<GetWarmerDescriptor, GetWarmerRequestParameters>, IGetWarmerRequest
	{
	}
}
