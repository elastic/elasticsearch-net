using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetRequest<T> : IRequest<GetRequestParameters>
		where T : class
	{
		
	}

	internal static class GetPathInfo
	{
		public static void Update<T>(ElasticsearchPathInfo<GetRequestParameters> pathInfo, IGetRequest<T> request)
			where T : class
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
		}
	}
	
	public partial class GetRequest<T> : DocumentPathBase<GetRequestParameters, T>, IGetRequest<T>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetRequestParameters> pathInfo)
		{
			GetPathInfo.Update(pathInfo, this);
		}
	}
	
	public partial class GetDescriptor<T> : DocumentPathDescriptorBase<GetDescriptor<T>,T, GetRequestParameters>, IGetRequest<T>
		where T : class
	{

		public GetDescriptor<T> ExecuteOnPrimary()
		{
			return this.Preference("_primary");
		}

		public GetDescriptor<T> ExecuteOnLocalShard()
		{
			return this.Preference("_local");
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetRequestParameters> pathInfo)
		{
			GetPathInfo.Update(pathInfo, this);
		}
	}
}
