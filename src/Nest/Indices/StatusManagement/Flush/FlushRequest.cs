using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFlushRequest : IRequest<FlushRequestParameters> { }

	public partial class FlushRequest : RequestBase<FlushRequestParameters>, IFlushRequest
	{
	}

	[DescriptorFor("IndicesFlush")]
	public partial class FlushDescriptor : RequestDescriptorBase<FlushDescriptor, FlushRequestParameters>, IFlushRequest
	{
	}
}
