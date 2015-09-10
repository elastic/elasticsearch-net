using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITemplateExistsRequest : INamePath<TemplateExistsRequestParameters> { }

	public partial class TemplateExistsRequest : NamePathBase<TemplateExistsRequestParameters>, ITemplateExistsRequest
	{
		public TemplateExistsRequest(string name) : base(name) { }
	}
	[DescriptorFor("IndicesExistsTemplate")]
	public partial class TemplateExistsDescriptor : NamePathDescriptor<TemplateExistsDescriptor, TemplateExistsRequestParameters>, ITemplateExistsRequest
	{
	}
}
