using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IInfoRequest : IRequest<InfoRequestParameters> { }

	internal static class InfoPathInfo
	{
		public static void Update(ElasticsearchPathInfo<InfoRequestParameters> pathInfo, IInfoRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.GET;
		}
	}
	
	public partial class InfoRequest : BasePathRequest<InfoRequestParameters>, IInfoRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<InfoRequestParameters> pathInfo)
		{
			InfoPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("Info")]
	public partial class InfoDescriptor : BasePathDescriptor<InfoDescriptor, InfoRequestParameters>, IInfoRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<InfoRequestParameters> pathInfo)
		{
			InfoPathInfo.Update(pathInfo, this);
		}
	}
}
