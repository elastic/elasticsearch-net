using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICloseIndexRequest : IIndexPath<CloseIndexRequestParameters> { }

	public partial class CloseIndexRequest : IndexPathBase<CloseIndexRequestParameters>, ICloseIndexRequest
	{
		public CloseIndexRequest(IndexName index) : base(index) { }
	}
	[DescriptorFor("IndicesClose")]
	public partial class CloseIndexDescriptor : IndexPathDescriptorBase<CloseIndexDescriptor, CloseIndexRequestParameters>, ICloseIndexRequest
	{
	}
}
