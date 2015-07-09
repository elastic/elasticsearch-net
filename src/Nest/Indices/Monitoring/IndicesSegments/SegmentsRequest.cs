using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface ISegmentsRequest : IIndicesOptionalPath<SegmentsRequestParameters> { }

	internal static class SegmentsPathInfo
	{
		public static void Update(IConnectionSettingsValues settings, ElasticsearchPathInfo<SegmentsRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}

	public partial class SegmentsRequest : IndicesOptionalPathBase<SegmentsRequestParameters>, ISegmentsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SegmentsRequestParameters> pathInfo)
		{
			SegmentsPathInfo.Update(settings, pathInfo);
		}
	}
	
	[DescriptorFor("IndicesSegments")]
	public partial class SegmentsDescriptor 
		: IndicesOptionalPathDescriptor<SegmentsDescriptor, SegmentsRequestParameters>, ISegmentsRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SegmentsRequestParameters> pathInfo)
		{
			SegmentsPathInfo.Update(settings, pathInfo);
		}
	}
}
