using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<CountRequest>))]
	public partial interface ICountRequest
	{
		[JsonProperty("query")]
		QueryContainer Query { get; set; }
	}
	public partial interface ICountRequest<T> : ICountRequest
		where T : class
	{

	}

	public partial class CountRequest
	{
		protected override HttpMethod HttpMethod =>
			Self.RequestParameters.ContainsKey("source") || Self.RequestParameters.ContainsKey("q") || Self.Query == null || Self.Query.IsConditionless()
				? HttpMethod.GET : HttpMethod.POST;

		public QueryContainer Query { get; set; }

	}

	public partial class CountRequest<T>
	{
		protected override HttpMethod HttpMethod =>
			Self.RequestParameters.ContainsKey("source") || Self.RequestParameters.ContainsKey("q") || Self.Query == null || Self.Query.IsConditionless()
				? HttpMethod.GET : HttpMethod.POST;

		public QueryContainer Query { get; set; }
	}

	[DescriptorFor("Count")]
	public partial class CountDescriptor<T> where T : class
	{
		protected override HttpMethod HttpMethod =>
			Self.RequestParameters.ContainsKey("source") || Self.RequestParameters.ContainsKey("q") || Self.Query == null || Self.Query.IsConditionless()
				? HttpMethod.GET : HttpMethod.POST;

		QueryContainer ICountRequest.Query { get; set; }

		public CountDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
