using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteSearchTemplateRequest : IRequest<DeleteTemplateRequestParameters>
	{
		//TODO NAME/TEMPLATE
	}

	public partial class DeleteSearchTemplateRequest : RequestBase<DeleteTemplateRequestParameters>, IDeleteSearchTemplateRequest
	{
	}


	public partial class DeleteSearchTemplateDescriptor 
		: RequestDescriptorBase<DeleteSearchTemplateDescriptor, DeleteTemplateRequestParameters>, IDeleteSearchTemplateRequest
	{
	}
}
