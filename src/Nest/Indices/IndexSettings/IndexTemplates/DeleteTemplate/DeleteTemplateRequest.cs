using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteTemplateRequest : INamePath<DeleteTemplateRequestParameters> { }

	public partial class DeleteTemplateRequest : NamePathBase<DeleteTemplateRequestParameters>, IDeleteTemplateRequest
	{
		public DeleteTemplateRequest(string name) : base(name) { }
	}
	[DescriptorFor("IndicesDeleteTemplate")]
	public partial class DeleteTemplateDescriptor : NamePathDescriptor<DeleteTemplateDescriptor, DeleteTemplateRequestParameters>, IDeleteTemplateRequest
	{
	}
}
