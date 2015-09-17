using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetFieldMappingRequest : IRequest<GetFieldMappingRequestParameters> { }
	public interface IGetFieldMappingRequest<T> : IGetFieldMappingRequest where T : class { }

	public partial class GetFieldMappingRequest : RequestBase<GetFieldMappingRequestParameters>, IGetFieldMappingRequest
	{
	}
	
	public partial class GetFieldMappingRequest<T> : RequestBase<GetFieldMappingRequestParameters>, IGetFieldMappingRequest
		where T : class
	{
	}

	[DescriptorFor("IndicesGetFieldMapping")]
	public partial class GetFieldMappingDescriptor<T> : RequestDescriptorBase<GetFieldMappingDescriptor<T>, GetFieldMappingRequestParameters>, IGetFieldMappingRequest
		where T : class
	{
	}
}
