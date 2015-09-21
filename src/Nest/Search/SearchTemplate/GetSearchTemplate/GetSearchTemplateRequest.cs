using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetSearchTemplateRequest : IRequest<GetTemplateRequestParameters>
	{
		//TODO NAME
	}

	public partial class GetSearchTemplateRequest : RequestBase<GetTemplateRequestParameters>, IGetSearchTemplateRequest
	{
	}


	public partial class GetSearchTemplateDescriptor : RequestDescriptorBase<GetSearchTemplateDescriptor, GetTemplateRequestParameters>, IGetSearchTemplateRequest
	{
	}
}
