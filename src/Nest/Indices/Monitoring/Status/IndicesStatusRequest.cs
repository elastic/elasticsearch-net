using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IIndicesStatusRequest : IIndicesOptionalPath<IndicesStatusRequestParameters> { }

	internal static class IndicesStatusPathInfo
	{
		public static void Update(ElasticsearchPathInfo<IndicesStatusRequestParameters> pathInfo, IIndicesStatusRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.GET;
		}
	}
	
	public partial class IndicesStatusRequest : IndicesOptionalPathBase<IndicesStatusRequestParameters>, IIndicesStatusRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndicesStatusRequestParameters> pathInfo)
		{
			IndicesStatusPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesStatus")]
	public partial class IndicesStatusDescriptor : IndicesOptionalPathDescriptor<IndicesStatusDescriptor, IndicesStatusRequestParameters>, IIndicesStatusRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndicesStatusRequestParameters> pathInfo)
		{
			IndicesStatusPathInfo.Update(pathInfo, this);
		}
	}
}
