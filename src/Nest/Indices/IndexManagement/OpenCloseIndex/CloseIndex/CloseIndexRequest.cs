using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICloseIndexRequest : IRequest<CloseIndexRequestParameters> { }

	public partial class CloseIndexRequest : RequestBase<CloseIndexRequestParameters>, ICloseIndexRequest
	{
	}

	[DescriptorFor("IndicesClose")]
	public partial class CloseIndexDescriptor : RequestDescriptorBase<CloseIndexDescriptor, CloseIndexRequestParameters>, ICloseIndexRequest
	{
	}
}
