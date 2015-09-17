using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IOptimizeRequest : IRequest<OptimizeRequestParameters> { }

	public partial class OptimizeRequest : RequestBase<OptimizeRequestParameters>, IOptimizeRequest
    {
	}

	[DescriptorFor("IndicesOptimize")]
	public partial class OptimizeDescriptor : RequestDescriptorBase<OptimizeDescriptor, OptimizeRequestParameters>, IOptimizeRequest
	{
	}
}
