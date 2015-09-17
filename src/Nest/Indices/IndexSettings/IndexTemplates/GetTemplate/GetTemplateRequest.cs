using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetTemplateRequest : IRequest<GetTemplateRequestParameters> { }

	public partial class GetTemplateRequest : RequestBase<GetTemplateRequestParameters>, IGetTemplateRequest
	{
	}

	[DescriptorFor("IndicesGetTemplate")]
	public partial class GetTemplateDescriptor : RequestDescriptorBase<GetTemplateDescriptor, GetTemplateRequestParameters>, IGetTemplateRequest
	{
	}
}
