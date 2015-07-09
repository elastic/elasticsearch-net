using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteMappingRequest : IIndexTypePath<DeleteMappingRequestParameters> { }
	public interface IDeleteMappingRequest<T> : IDeleteMappingRequest where T : class { }

	internal static class DeleteMappingPathInfo
	{
		public static void Update(ElasticsearchPathInfo<DeleteMappingRequestParameters> pathInfo, IDeleteMappingRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
	
	public partial class DeleteMappingRequest : IndexTypePathBase<DeleteMappingRequestParameters>, IDeleteMappingRequest
	{
		public DeleteMappingRequest(IndexName index, TypeName typeNameMarker) : base(index, typeNameMarker)
		{
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteMappingRequestParameters> pathInfo)
		{
			DeleteMappingPathInfo.Update(pathInfo, this);
		}
	}
	public partial class DeleteMappingRequest<T> : IndexTypePathBase<DeleteMappingRequestParameters, T>, IDeleteMappingRequest
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteMappingRequestParameters> pathInfo)
		{
			DeleteMappingPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesDeleteMapping")]
	public partial class DeleteMappingDescriptor<T> : IndexTypePathDescriptor<DeleteMappingDescriptor<T>, DeleteMappingRequestParameters, T>, IDeleteMappingRequest
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteMappingRequestParameters> pathInfo)
		{
			DeleteMappingPathInfo.Update(pathInfo, this);
		}
	}
}
