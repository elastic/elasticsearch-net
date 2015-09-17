using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetIndexSettingsRequest : IRequest<GetIndexSettingsRequestParameters> { }

	public partial class GetIndexSettingsRequest : RequestBase<GetIndexSettingsRequestParameters>, IGetIndexSettingsRequest
	{
	}

	[DescriptorFor("IndicesGetSettings")]
	public partial class GetIndexSettingsDescriptor 
		: RequestDescriptorBase<GetIndexSettingsDescriptor, GetIndexSettingsRequestParameters>, IGetIndexSettingsRequest
	{
	}
}
