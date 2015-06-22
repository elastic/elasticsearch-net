using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IIndexExistsRequest : IIndexPath<IndexExistsRequestParameters> { }

	internal static class IndexExistsPathInfo
	{
		public static void Update(ElasticsearchPathInfo<IndexExistsRequestParameters> pathInfo, IIndexExistsRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.HEAD;
		}
	}
	
	public partial class IndexExistsRequest : IndexPathBase<IndexExistsRequestParameters>, IIndexExistsRequest
	{
		public IndexExistsRequest(IndexNameMarker index) : base(index) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexExistsRequestParameters> pathInfo)
		{
			IndexExistsPathInfo.Update(pathInfo, this);
		}
	}
	[DescriptorFor("IndicesExists")]
	public partial class IndexExistsDescriptor : IndexPathDescriptorBase<IndexExistsDescriptor, IndexExistsRequestParameters>, IIndexExistsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexExistsRequestParameters> pathInfo)
		{
			IndexExistsPathInfo.Update(pathInfo, this);
		}
	}
}
