using System;
using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("count.json")]
	[ReadAs(typeof(CountRequest))]
	public partial interface ICountRequest
	{
		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }
	}

	public partial interface ICountRequest<T> where T : class { }

	public partial class CountRequest
	{
		public QueryContainer Query { get; set; }

		protected override HttpMethod HttpMethod =>
			Self.RequestParameters.ContainsQueryString("source") || Self.RequestParameters.ContainsQueryString("q") || Self.Query == null
			|| Self.Query.IsConditionless()
				? HttpMethod.GET
				: HttpMethod.POST;
	}

	public partial class CountRequest<T> where T : class
	{
	}

	public partial class CountDescriptor<T> where T : class
	{
		protected override HttpMethod HttpMethod =>
			Self.RequestParameters.ContainsQueryString("source") || Self.RequestParameters.ContainsQueryString("q") || Self.Query == null
			|| Self.Query.IsConditionless()
				? HttpMethod.GET
				: HttpMethod.POST;

		QueryContainer ICountRequest.Query { get; set; }

		public CountDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
