using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IOpenIndexRequest : IRequest<OpenIndexRequestParameters> { }

	public partial class OpenIndexRequest : RequestBase<OpenIndexRequestParameters>, IOpenIndexRequest
	{
		public OpenIndexRequest(IndexName index) : base(index) { }
	}

	[DescriptorFor("IndicesOpen")]
	public partial class OpenIndexDescriptor : RequestDescriptorBase<OpenIndexDescriptor, OpenIndexRequestParameters>, IOpenIndexRequest
	{
	}
}
