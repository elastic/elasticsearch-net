using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetMappingRequest : IRequest<GetMappingRequestParameters> { }

	internal static class GetMappingPathInfo
	{
		public static void Update(ElasticsearchPathInfo<GetMappingRequestParameters> pathInfo, IGetMappingRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
	
	public partial class GetMappingRequest : IndexTypePathBase<GetMappingRequestParameters>, IGetMappingRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetMappingRequestParameters> pathInfo)
		{
			GetMappingPathInfo.Update(pathInfo, this);
		}
	}
	[DescriptorFor("IndicesGetMapping")]
	public partial class GetMappingDescriptor : IndexTypePathDescriptor<GetMappingDescriptor, GetMappingRequestParameters>, IGetMappingRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetMappingRequestParameters> pathInfo)
		{
			GetMappingPathInfo.Update(pathInfo, this);
		}
	}
}
