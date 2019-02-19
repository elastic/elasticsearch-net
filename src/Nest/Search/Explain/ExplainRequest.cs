using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("explain.json")]
	public partial interface IExplainRequest<TDocument> where TDocument : class
	{
		[JsonProperty("query")]
		QueryContainer Query { get; set; }
	}

	public partial class ExplainRequest<TDocument> where TDocument : class
	{
		public QueryContainer Query { get; set; }

		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsQueryString("source") == true || RequestState.RequestParameters?.ContainsQueryString("q") == true
				? HttpMethod.GET
				: HttpMethod.POST;

		private object AutoRouteDocument() => null;
	}

	public partial class ExplainDescriptor<TDocument> where TDocument : class
	{
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsQueryString("source") == true || RequestState.RequestParameters?.ContainsQueryString("q") == true
				? HttpMethod.GET
				: HttpMethod.POST;

		QueryContainer IExplainRequest<TDocument>.Query { get; set; }

		private object AutoRouteDocument() => null;

		public ExplainDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> querySelector) =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<TDocument>()));

		//TODO write a code standards tests for Field/Fields descriptors (if not already exists)
		/// <summary>
		/// Allows to selectively load specific fields for each document
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public ExplainDescriptor<TDocument> StoredFields(Func<FieldsDescriptor<TDocument>, IPromise<Fields>> fields) =>
			StoredFields(fields?.Invoke(new FieldsDescriptor<TDocument>())?.Value);
	}
}
