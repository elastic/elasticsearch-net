using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITemplateExistsRequest : IRequest<TemplateExistsRequestParameters> { }

	public partial class TemplateExistsRequest : RequestBase<TemplateExistsRequestParameters>, ITemplateExistsRequest
	{
	}

	[DescriptorFor("IndicesExistsTemplate")]
	public partial class TemplateExistsDescriptor : RequestDescriptorBase<TemplateExistsDescriptor, TemplateExistsRequestParameters>, ITemplateExistsRequest
	{
	}
}
