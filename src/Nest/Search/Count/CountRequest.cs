using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICountRequest : IRequest<CountRequestParameters>
	{
		[JsonProperty("query")]
		IQueryContainer Query { get; set; }
	}
	public interface ICountRequest<T> : ICountRequest where T : class {}

	//TODO port this HttpMethod logic to property
	//internal static class CountPathInfo
	//{
	//	public static void Update(RouteValues pathInfo, ICountRequest request)
	//	{
	//		var source = request.RequestParameters.GetQueryStringValue<string>("source");
	//		pathInfo.HttpMethod = source.IsNullOrEmpty() 
	//			&& (request.Query == null || request.Query.IsConditionless)
	//			? HttpMethod.GET
	//			: HttpMethod.POST;
	//	}
	//}
	
	public partial class CountRequest : RequestBase<CountRequestParameters>, ICountRequest
	{
		public IQueryContainer Query { get; set; }
	}

	public partial class CountRequest<T> : RequestBase<CountRequestParameters>, ICountRequest<T>
		where T : class
	{
		public IQueryContainer Query { get; set; }
	}
	
	[DescriptorFor("Count")]
	public partial class CountDescriptor<T> : RequestDescriptorBase<CountDescriptor<T>, CountRequestParameters>, ICountRequest<T>
		where T : class
	{
		private ICountRequest Self => this;

		IQueryContainer ICountRequest.Query { get; set; }

		public CountDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
		{
			Self.Query = querySelector(new QueryContainerDescriptor<T>());
			return this;
		}
	}
}
