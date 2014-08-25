using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	internal static class PingRequestPathInfo
	{
		public static void Update(ElasticsearchPathInfo<PingRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.HEAD;
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPingRequest : IRequest<PingRequestParameters>
	{
	}

	public partial class PingRequest : BasePathRequest<PingRequestParameters>, IPingRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PingRequestParameters> pathInfo)
		{
			PingRequestPathInfo.Update(pathInfo);
		}
	}

	public partial class PingDescriptor : BasePathDescriptor<PingDescriptor, PingRequestParameters>, IPingRequest
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PingRequestParameters> pathInfo)
		{
			PingRequestPathInfo.Update(pathInfo);
		}
	}
}
