using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetIndexSettingsRequest : IIndicesOptionalPath<GetIndexSettingsRequestParameters> { }

	public partial class GetIndexSettingsRequest : IndicesOptionalPathBase<GetIndexSettingsRequestParameters>, IGetIndexSettingsRequest
	{
		public GetIndexSettingsRequest() { }
		public GetIndexSettingsRequest(IndexName index) { this.Indices = new []{ index }; }
	}

	[DescriptorFor("IndicesGetSettings")]
	public partial class GetIndexSettingsDescriptor 
		: IndicesOptionalPathDescriptor<GetIndexSettingsDescriptor, GetIndexSettingsRequestParameters>, IGetIndexSettingsRequest
	{
	}
}
