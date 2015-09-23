using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface ICountRequest 
	{
		[JsonProperty("query")]
		IQueryContainer Query { get; set; }
	}
	//TODO removed typed variant of request assert this is ok in new setup

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
	
	public partial class CountRequest 
	{
		public IQueryContainer Query { get; set; }
	}

	[DescriptorFor("Count")]
	public partial class CountDescriptor<T> where T : class
	{
		IQueryContainer ICountRequest.Query { get; set; }

		public CountDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
