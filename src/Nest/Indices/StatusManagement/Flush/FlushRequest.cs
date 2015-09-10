using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFlushRequest : IIndicesOptionalExplicitAllPath<FlushRequestParameters> { }

	public partial class FlushRequest : IndicesOptionalExplicitAllPathBase<FlushRequestParameters>, IFlushRequest
	{
		public FlushRequest(Indices indices) : base(indices) { }
	}
	[DescriptorFor("IndicesFlush")]
	public partial class FlushDescriptor : IndicesOptionalExplicitAllPathDescriptor<FlushDescriptor, FlushRequestParameters>, IFlushRequest
	{
		public FlushDescriptor(Indices indices) : base(indices) { }
	}
}
