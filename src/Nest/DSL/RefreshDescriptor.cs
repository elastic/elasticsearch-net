using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRefreshRequest : IIndicesOptionalPath<RefreshRequestParameters> { }

	internal static class RefreshPathInfo
	{
		public static void Update(ElasticsearchPathInfo<RefreshRequestParameters> pathInfo, IRefreshRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class RefreshRequest : IndicesOptionalPathBase<RefreshRequestParameters>, IRefreshRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<RefreshRequestParameters> pathInfo)
		{
			RefreshPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesRefresh")]
	public partial class RefreshDescriptor : IndicesOptionalPathDescriptor<RefreshDescriptor, RefreshRequestParameters>, IRefreshRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<RefreshRequestParameters> pathInfo)
		{
			RefreshPathInfo.Update(pathInfo, this);
		}
	}
}
