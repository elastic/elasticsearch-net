using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IOptimizeRequest : IIndicesOptionalPath<OptimizeRequestParameters> { }

	internal static class OptimizePathInfo
	{
		public static void Update(ElasticsearchPathInfo<OptimizeRequestParameters> pathInfo, IOptimizeRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class OptimizeRequest : IndicesOptionalPathBase<OptimizeRequestParameters>, IOptimizeRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<OptimizeRequestParameters> pathInfo)
		{
			OptimizePathInfo.Update(pathInfo, this);
		}
	}
	[DescriptorFor("IndicesOptimize")]
	public partial class OptimizeDescriptor : IndicesOptionalPathDescriptor<OptimizeDescriptor, OptimizeRequestParameters>, IOptimizeRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<OptimizeRequestParameters> pathInfo)
		{
			OptimizePathInfo.Update(pathInfo, this);
		}
	}
}
