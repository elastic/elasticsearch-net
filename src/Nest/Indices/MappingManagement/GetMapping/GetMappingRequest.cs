using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetMappingRequest : IRequest<GetMappingRequestParameters> { }
	public interface IGetMappingRequest<T> : IGetMappingRequest where T : class { }

	public partial class GetMappingRequest : RequestBase<GetMappingRequestParameters>, IGetMappingRequest
	{
	}
	
	public partial class GetMappingRequest<T> : RequestBase<GetMappingRequestParameters>, IGetMappingRequest<T>
		where T : class
	{
	}

	[DescriptorFor("IndicesGetMapping")]
	public partial class GetMappingDescriptor<T> : RequestDescriptorBase<GetMappingDescriptor<T>, GetMappingRequestParameters>, IGetMappingRequest
		where T : class
	{
	}
}
