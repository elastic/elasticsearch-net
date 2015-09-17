using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteWarmerRequest : IRequest<DeleteWarmerRequestParameters> { }

	public partial class DeleteWarmerRequest : RequestBase<DeleteWarmerRequestParameters>, IDeleteWarmerRequest
	{
	}

	[DescriptorFor("IndicesDeleteWarmer")]
	public partial class DeleteWarmerDescriptor : RequestDescriptorBase<DeleteWarmerDescriptor, DeleteWarmerRequestParameters>, IDeleteWarmerRequest
	{
	}
}
