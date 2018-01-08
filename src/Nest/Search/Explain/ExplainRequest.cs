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
			RequestState.RequestParameters?.ContainsQueryString("source") == true || RequestState.RequestParameters?.ContainsQueryString("q")  == true? HttpMethod.GET : HttpMethod.POST;

		public Fields StoredFields { get; set; }
		public QueryContainer Query { get; set; }
		private object AutoRouteDocument() => null;
	}

	[DescriptorFor("Explain")]
	public partial class ExplainDescriptor<TDocument> : IExplainRequest<TDocument>
		where TDocument : class
	{
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsQueryString("source") == true || RequestState.RequestParameters?.ContainsQueryString("q")  == true? HttpMethod.GET : HttpMethod.POST;

		private object AutoRouteDocument() => null;

		Fields IExplainRequest<TDocument>.StoredFields { get; set; }
		QueryContainer IExplainRequest<TDocument>.Query { get; set; }

		public ExplainDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> querySelector) =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<TDocument>()));

		/// <summary>
		/// Allows to selectively load specific fields for each document
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public ExplainDescriptor<TDocument> StoredFields(Func<FieldsDescriptor<TDocument>, IPromise<Fields>> fields) =>
			Assign(a => a.StoredFields = fields?.Invoke(new FieldsDescriptor<TDocument>())?.Value);

		public ExplainDescriptor<TDocument> StoredFields(Fields fields) => Assign(a => a.StoredFields = fields);
	}
}
