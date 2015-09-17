using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteTemplateRequest : IRequest<DeleteTemplateRequestParameters> { }

	public partial class DeleteTemplateRequest : RequestBase<DeleteTemplateRequestParameters>, IDeleteTemplateRequest
	{
	}

	[DescriptorFor("IndicesDeleteTemplate")]
	public partial class DeleteTemplateDescriptor : RequestDescriptorBase<DeleteTemplateDescriptor, DeleteTemplateRequestParameters>, IDeleteTemplateRequest
	{
	}
}
