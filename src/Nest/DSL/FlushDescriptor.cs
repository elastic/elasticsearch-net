using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFlushRequest : IIndicesOptionalExplicitAllPath<FlushRequestParameters> { }

	internal static class FlushPathInfo
	{
		public static void Update(ElasticsearchPathInfo<FlushRequestParameters> pathInfo, IFlushRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class FlushRequest : IndicesOptionalExplicitAllPathBase<FlushRequestParameters>, IFlushRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<FlushRequestParameters> pathInfo)
		{
			FlushPathInfo.Update(pathInfo, this);
		}
	}
	[DescriptorFor("IndicesFlush")]
	public partial class FlushDescriptor : IndicesOptionalExplicitAllPathDescriptor<FlushDescriptor, FlushRequestParameters>, IFlushRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<FlushRequestParameters> pathInfo)
		{
			FlushPathInfo.Update(pathInfo, this);
		}
	}
}
