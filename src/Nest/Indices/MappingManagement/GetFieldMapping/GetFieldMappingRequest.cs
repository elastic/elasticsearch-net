using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetFieldMappingRequest : IIndicesOptionalTypesOptionalFieldsPath<GetFieldMappingRequestParameters> { }
	public interface IGetFieldMappingRequest<T> : IGetFieldMappingRequest where T : class { }

	public partial class GetFieldMappingRequest : IndicesOptionalTypesOptionalFieldsPathBase<GetFieldMappingRequestParameters>, IGetFieldMappingRequest
	{
		public GetFieldMappingRequest(params FieldName[] fields) : base(fields) {}
	}
	
	public partial class GetFieldMappingRequest<T> : IndicesOptionalTypesOptionalFieldsPathBase<GetFieldMappingRequestParameters, T>, IGetFieldMappingRequest
		where T : class
	{
		public GetFieldMappingRequest(params FieldName[] fields) : base(fields) { }

		public GetFieldMappingRequest(params Expression<Func<T, object>>[] fields) : base(fields) { }
	}

	[DescriptorFor("IndicesGetFieldMapping")]
	public partial class GetFieldMappingDescriptor<T> : IndicesOptionalTypesOptionalFieldsPathDescriptor<GetFieldMappingDescriptor<T>, GetFieldMappingRequestParameters, T>, IGetFieldMappingRequest
		where T : class
	{
	}
}
