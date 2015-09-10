using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetTemplateRequest : INamePath<GetTemplateRequestParameters> { }

	public partial class GetTemplateRequest : NamePathBase<GetTemplateRequestParameters>, IGetTemplateRequest
	{
		public GetTemplateRequest(string name) : base(name) { }
	}

	[DescriptorFor("IndicesGetTemplate")]
	public partial class GetTemplateDescriptor : NamePathDescriptor<GetTemplateDescriptor, GetTemplateRequestParameters>, IGetTemplateRequest
	{
	}
}
