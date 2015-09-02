using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetIndexSettingsRequest : IIndicesOptionalPath<GetIndexSettingsRequestParameters> { }

	internal static class GetIndexSettingsPathInfo
	{
		public static void Update(ElasticsearchPathInfo<GetIndexSettingsRequestParameters> pathInfo, IGetIndexSettingsRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.GET;
		}
	}
	
	public partial class GetIndexSettingsRequest : IndicesOptionalPathBase<GetIndexSettingsRequestParameters>, IGetIndexSettingsRequest
	{
		public GetIndexSettingsRequest() { }
		public GetIndexSettingsRequest(IndexName index) { this.Indices = new []{ index }; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetIndexSettingsRequestParameters> pathInfo)
		{
			GetIndexSettingsPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesGetSettings")]
	public partial class GetIndexSettingsDescriptor 
		: IndicesOptionalPathDescriptor<GetIndexSettingsDescriptor, GetIndexSettingsRequestParameters>, IGetIndexSettingsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetIndexSettingsRequestParameters> pathInfo)
		{
			GetIndexSettingsPathInfo.Update(pathInfo, this);
		}
	}
}
