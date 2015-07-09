using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICloseIndexRequest : IIndexPath<CloseIndexRequestParameters> { }

	internal static class CloseIndexPathInfo
	{
		public static void Update(ElasticsearchPathInfo<CloseIndexRequestParameters> pathInfo, ICloseIndexRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class CloseIndexRequest : IndexPathBase<CloseIndexRequestParameters>, ICloseIndexRequest
	{
		public CloseIndexRequest(IndexName index) : base(index) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CloseIndexRequestParameters> pathInfo)
		{
			CloseIndexPathInfo.Update(pathInfo, this);
		}
	}
	[DescriptorFor("IndicesClose")]
	public partial class CloseIndexDescriptor : IndexPathDescriptorBase<CloseIndexDescriptor, CloseIndexRequestParameters>, ICloseIndexRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CloseIndexRequestParameters> pathInfo)
		{
			CloseIndexPathInfo.Update(pathInfo, this);
		}
	}
}
