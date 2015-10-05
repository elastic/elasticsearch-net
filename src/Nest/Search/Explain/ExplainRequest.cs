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

	public partial class ExplainRequest<TDocument> : IExplainRequest<TDocument>
		where TDocument : class
	{
		protected override HttpMethod HttpMethod =>
			Self.RequestParameters?.ContainsKey("_source") == true || Self.RequestParameters?.ContainsKey("q")  == true? HttpMethod.GET : HttpMethod.POST;

		public IQueryContainer Query { get; set; }
	}

	[DescriptorFor("Explain")]
	public partial class ExplainDescriptor<TDocument> : IExplainRequest<TDocument>
		where TDocument : class
	{
		protected override HttpMethod HttpMethod =>
			Self.RequestParameters?.ContainsKey("_source") == true || Self.RequestParameters?.ContainsKey("q")  == true? HttpMethod.GET : HttpMethod.POST;

		IQueryContainer IExplainRequest.Query { get; set; }

		public ExplainDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> querySelector) => 
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<TDocument>()));
	}
}
