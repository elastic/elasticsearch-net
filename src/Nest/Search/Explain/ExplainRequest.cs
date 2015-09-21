using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IExplainRequest 
	{
		[JsonProperty("query")]
		IQueryContainer Query { get; set; }
	}

	//TODO Removed typed variant assert this is ok using new setup

	//TODO port this HttpMethod logic to property
	//internal static class ExplainPathInfo
	//{
	//	public static void Update(RouteValues pathInfo, IExplainRequest request)
	//	{
	//		var source = request.RequestParameters.GetQueryStringValue<string>("source");
	//		var q = request.RequestParameters.GetQueryStringValue<string>("q");
	//		pathInfo.HttpMethod = (!source.IsNullOrEmpty() || !q.IsNullOrEmpty())
	//			? HttpMethod.GET
	//			: HttpMethod.POST;
	//	}
	//}

	public partial class ExplainRequest 
	{
		public IQueryContainer Query { get; set; }
	}

	[DescriptorFor("Explain")]
	public partial class ExplainDescriptor<T> where T : class
	{
		IQueryContainer IExplainRequest.Query { get; set; }

		public ExplainDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) => 
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
