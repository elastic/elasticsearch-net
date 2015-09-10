using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IIndexExistsRequest : IIndexPath<IndexExistsRequestParameters> { }

	public partial class IndexExistsRequest : IndexPathBase<IndexExistsRequestParameters>, IIndexExistsRequest
	{
		public IndexExistsRequest(IndexName index) : base(index) { }
	}
	[DescriptorFor("IndicesExists")]
	public partial class IndexExistsDescriptor : IndexPathDescriptorBase<IndexExistsDescriptor, IndexExistsRequestParameters>, IIndexExistsRequest { }
}
