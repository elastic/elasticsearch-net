using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetIndexSettingsRequest : IIndexPath<GetIndexSettingsRequestParameters> { }

	internal static class GetIndexSettingsPathInfo
	{
		public static void Update(ElasticsearchPathInfo<GetIndexSettingsRequestParameters> pathInfo, IGetIndexSettingsRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
	
	public partial class GetIndexSettingsRequest : IndexPathBase<GetIndexSettingsRequestParameters>, IGetIndexSettingsRequest
	{
		public GetIndexSettingsRequest(IndexNameMarker index) : base(index) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetIndexSettingsRequestParameters> pathInfo)
		{
			GetIndexSettingsPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesGetSettings")]
	public partial class GetIndexSettingsDescriptor : IndexPathDescriptorBase<GetIndexSettingsDescriptor, GetIndexSettingsRequestParameters>, IGetIndexSettingsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetIndexSettingsRequestParameters> pathInfo)
		{
			GetIndexSettingsPathInfo.Update(pathInfo, this);
		}
	}
}
