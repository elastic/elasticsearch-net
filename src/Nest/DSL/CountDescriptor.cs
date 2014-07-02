using System;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICountRequest<T> : IRequest<CountRequestParameters>
		where T : class
	{
		
		[JsonProperty("query")]
		IQueryContainer Query { get; set; }
	}

	internal static class CountPathInfo
	{
		public static void Update<T>(ElasticsearchPathInfo<CountRequestParameters> pathInfo, ICountRequest<T> request)
			where T : class
		{
			var source = request.RequestParameters.GetQueryStringValue<string>("source");
			pathInfo.HttpMethod = !source.IsNullOrEmpty() 
				? PathInfoHttpMethod.GET
				: PathInfoHttpMethod.POST;
		
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class CountRequest<T> : QueryPathBase<CountRequestParameters, T>, ICountRequest<T>
		where T : class
	{
		public IQueryContainer Query { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CountRequestParameters> pathInfo)
		{
			CountPathInfo.Update(pathInfo, this);
		}
	}
	
	[DescriptorFor("Count")]
	public partial class CountDescriptor<T> : QueryPathDescriptorBase<CountDescriptor<T>, T, CountRequestParameters>, ICountRequest<T>
		where T : class
	{
		private ICountRequest<T> Self { get { return this; } }

		IQueryContainer ICountRequest<T>.Query { get; set; }

		public CountDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			Self.Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<CountRequestParameters> pathInfo)
		{
			CountPathInfo.Update(pathInfo, this);
		}
	}
}
