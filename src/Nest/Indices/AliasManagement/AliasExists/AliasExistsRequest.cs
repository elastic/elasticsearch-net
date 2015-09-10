using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAliasExistsRequest : IIndexOptionalNamePath<AliasExistsRequestParameters> { }

	public partial class AliasExistsRequest : IndexOptionalNamePathBase<AliasExistsRequestParameters>, IAliasExistsRequest
	{
		public AliasExistsRequest(string name) : base(name) { }
		public AliasExistsRequest(IndexName index, string name) : base(index, name) { }
	}
	[DescriptorFor("IndicesExistsAlias")]
	public partial class AliasExistsDescriptor : IndexOptionalNamePathDescriptor<AliasExistsDescriptor, AliasExistsRequestParameters>, IAliasExistsRequest
	{
	}
}
