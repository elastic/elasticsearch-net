using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IClearCacheRequest : IIndicesOptionalPath<ClearCacheRequestParameters> { }

	internal static class ClearCachePathInfo
	{
		public static void Update(ElasticsearchPathInfo<ClearCacheRequestParameters> pathInfo, IClearCacheRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class ClearCacheRequest : IndicesOptionalPathBase<ClearCacheRequestParameters>, IClearCacheRequest
	{
        [Obsolete("Use FilterCacheKeys to set filter_keys", true)]
        public bool FilterKeys { get; set; }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClearCacheRequestParameters> pathInfo)
		{
			ClearCachePathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesClearCache")]
	public partial class ClearCacheDescriptor : IndicesOptionalPathDescriptor<ClearCacheDescriptor, ClearCacheRequestParameters>, IClearCacheRequest
	{
        [Obsolete("Use FilterKeys(params string[] filter_keys)", true)]
	    public ClearCacheDescriptor FilterKeys(bool filter_keys = true)
        {
            return this;
        }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<ClearCacheRequestParameters> pathInfo)
		{
			ClearCachePathInfo.Update(pathInfo, this);
		}
	}
}
