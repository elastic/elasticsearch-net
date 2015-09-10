using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetMappingRequest : IIndexTypePath<GetMappingRequestParameters> { }
	public interface IGetMappingRequest<T> : IGetMappingRequest where T : class { }

	public partial class GetMappingRequest : IndexTypePathBase<GetMappingRequestParameters>, IGetMappingRequest
	{
		public GetMappingRequest(IndexName index, TypeName typeNameMarker) : base(index, typeNameMarker) { }
	}
	
	public partial class GetMappingRequest<T> : IndexTypePathBase<GetMappingRequestParameters, T>, IGetMappingRequest
		where T : class
	{
	}

	[DescriptorFor("IndicesGetMapping")]
	public partial class GetMappingDescriptor<T> : IndexTypePathDescriptor<GetMappingDescriptor<T>, GetMappingRequestParameters, T>, IGetMappingRequest
		where T : class
	{
	}
}
