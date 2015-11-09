using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IExplainRequest<TDocument> where TDocument : class
	{
		[JsonProperty("query")]
		QueryContainer Query { get; set; }
	}

	public partial class ExplainRequest<TDocument> : IExplainRequest<TDocument>
		where TDocument : class
	{
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsKey("_source") == true || RequestState.RequestParameters?.ContainsKey("q")  == true? HttpMethod.GET : HttpMethod.POST;

		public QueryContainer Query { get; set; }
	}

	[DescriptorFor("Explain")]
	public partial class ExplainDescriptor<TDocument> : IExplainRequest<TDocument>
		where TDocument : class
	{
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsKey("_source") == true || RequestState.RequestParameters?.ContainsKey("q")  == true? HttpMethod.GET : HttpMethod.POST;

		QueryContainer IExplainRequest<TDocument>.Query { get; set; }

		public ExplainDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> querySelector) => 
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<TDocument>()));
	}
}
