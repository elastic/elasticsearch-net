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
	public partial interface IExplainRequest<TDocument> : IExplainRequest where TDocument : class { }

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

	public partial class ExplainRequest<TDocument> : IExplainRequest<TDocument>
		where TDocument : class
	{
		public IQueryContainer Query { get; set; }
	}

	[DescriptorFor("Explain")]
	public partial class ExplainDescriptor<TDocument> : IExplainRequest<TDocument>
		where TDocument : class
	{
		IQueryContainer IExplainRequest.Query { get; set; }

		public ExplainDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> querySelector) => 
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<TDocument>()));
	}
}
